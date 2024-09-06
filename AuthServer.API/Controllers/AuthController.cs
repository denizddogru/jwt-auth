using AuthServer.Core.Dtos;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers;


[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : CustomBaseController
{

    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {

        _authenticationService = authenticationService;
    }

    // api/auth/createtoken
    [HttpPost]
    public async Task<IActionResult> CreateToken(LoginDto loginDto)
    {
        var result = await _authenticationService.CreateTokenAsync(loginDto);

        // result.StatusCode == 200 
        // else if result.StatusCode == 404
        // not Found gibi if else gerek kalmadan ObjectResult'ı döndük. Bunun için de CustomBaseController'ı inherit ettik
        return ActionResultInstance(result);
    }

    [HttpPost]

    // Üyelik işlemi gerektirmeyen API'ler için bu endpointini tanımladık.
    // Yani user,pass olmadan istek atmak istiyoruz.

    // 1.Adım ClientLoginDto ile bir ClientId ve ClientSecret parametresi istiyoruz. ( Auth serverımızın kabul edeceği parametreleri appsettings'de tanımladık.)
    // 2.Adım AuthServer'dan Token almak için istek atıyoruz. ( Attığımız istek başarılıysa dönen Token'ı jwt.io'ya yapıştırdığımız zaman Issuer ve Audience'ı görebiliriz. ( Issuer Token'ı veren AuthServer, 
    //                                                                                                                                                                        Audience: Token'ın erişimi olan API'lar ( miniApi2 )
    // 3. Aldığımız bu token bilgisini gidip miniAPI2'nin GetInvoice metoduna istek atmak için kullandık. 
    public IActionResult CreateTokenByClient(ClientLoginDto clientLoginDto)
    {
        var result = _authenticationService.CreateTokenByClient(clientLoginDto);

        return ActionResultInstance(result);
    }

    [HttpPost]
    public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.RefreshToken);

        return ActionResultInstance(result);
    }

    [HttpPost]

    public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var result = await _authenticationService.CreateTokenByRefreshTokenAsync(refreshTokenDto.RefreshToken);

        return ActionResultInstance(result);
    }
}

