using AuthServer.Core.Configuration;
using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.AspNetCore.Identity;
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


    public Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
