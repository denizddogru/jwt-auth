using AuthServer.Core.Configuration;
using AuthServer.Core.Dtos;
using AuthServer.Core.Entity;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

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

    // part 1:
    // 32-bytelık random bir string ürettik.
    private string CreateRefreshToken()
    {
        var numberByte = new Byte[32];

        using var rnd = RandomNumberGenerator.Create();

        rnd.GetBytes(numberByte);

        return Convert.ToBase64String(numberByte);


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

    // part 3: Üyelik sistemi gerektirmeyen bir token oluşturmak istediğimizde bu servisi kullanacağız.
    private IEnumerable<Claim> GetClaimsByClient(Client client)
    {
        var claims = new List<Claim>();
        claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
        new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

        return claims;
    }

    // part 4:
    public TokenDto CreateToken(AppUser appUser)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.RefreshTokenExpiration);

        var securityKey = SignService.GetSymmetricSecurityKey(_tokenOptions.SecurityKey);

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: GetClaim(appUser, _tokenOptions.Audience),
            signingCredentials: signingCredentials);

        var handler = new JwtSecurityTokenHandler();

        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDto
        {
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            AccessTokenExpirationDate = accessTokenExpiration,
            RefreshTokenExpirationDate = refreshTokenExpiration,
        };

        return tokenDto;
    }

    public ClientTokenDto CreateTokenByClient(Client client)
    {
        throw new NotImplementedException();
    }
}
