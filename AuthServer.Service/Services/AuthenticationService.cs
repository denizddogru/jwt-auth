using AuthServer.Core.Configuration;
using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;

namespace AuthServer.Service.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly List<Client> _clients;
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

    public AuthenticationService(IOptions<List<Client>> optionsClient,
        ITokenService tokenService,
        UserManager<AppUser> userManager,
        IUnitOfWork unitOfWork,
        IGenericRepository<UserRefreshToken> userRefreshToken)
    {
        _clients = optionsClient.Value;
        _tokenService = tokenService;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _userRefreshTokenService = userRefreshToken;
    }


    public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        if(loginDto==null)
            throw new ArgumentNullException(nameof(loginDto));

        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
            return Response<TokenDto>.Fail("Email or Password incorrect", 400, true);

        if(!await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            return Response<TokenDto>.Fail("Email or Password incorrect", 400, true);

        }

        var token = _tokenService.CreateToken(user);

        var userRefreshToken = await _userRefreshTokenService.Where
            (x => x.UserId == user.Id).SingleOrDefaultAsync();

        if(userRefreshToken != null)
        {
            await _userRefreshTokenService.AddAsync(new UserRefreshToken { 
                UserId =user.Id, RefreshToken=token.RefreshToken,
                ExpirationDate=token.RefreshTokenExpirationDate });
        }
        else
        {
            userRefreshToken.RefreshToken=token.RefreshToken;
            userRefreshToken.ExpirationDate = token.RefreshTokenExpirationDate;

        }

        await _unitOfWork.CommitAsync();

        return Response<TokenDto>.Success(200);

    }

    public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
    {
        var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

        if(client==null)
        {
            return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found", 404, true);
        }

        var token = _tokenService.CreateTokenByClient(client);
        return Response<ClientTokenDto>.Success(200);
    }

    public Response<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
