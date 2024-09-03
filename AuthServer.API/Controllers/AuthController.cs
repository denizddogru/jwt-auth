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

