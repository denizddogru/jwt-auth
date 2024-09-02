using AuthServer.Core.Configuration;
using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using SharedLibrary.Configurations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthServer.Service.Services;
public class TokenService : ITokenService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly CustomTokenOptions _tokenOptions;

    public TokenService(UserManager<AppUser> userManager, IOptions<CustomTokenOptions> tokenOptions)
    {
        _userManager = userManager;
        _tokenOptions = tokenOptions.Value;
    }

    private string CreateRefreshToken()
    {
        var numberByte = new Byte[32];

        using var rnd = RandomNumberGenerator.Create();

        rnd.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);

        // part 1:
        // 32-bytelık random bir string ürettik.
    }

    // part 2: Claim nesneleri oluşturuyoruz.

    // Claim: Token içerisinde Payloadda ( data ) data taşınan tüm bilgiler claimler olur.
    // claim : type, value
    // Aynı zamanda token ile ilgili diğer bilgiler de claim.

    private IEnumerable<Claim> GetClaim(AppUser appUser, List<string> audiences)
    {
        var userList = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id),
            new Claim(JwtRegisteredClaimNames.Email ,appUser.Email),
            new Claim(ClaimTypes.Name, appUser.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        return userList;
    }


    public TokenDto CreateToken(AppUser appUser)
    {
        throw new NotImplementedException();
    }
    
    public ClientTokenDto CreateTokenByClient(Client client)
    {
        throw new NotImplementedException();
    }
}
