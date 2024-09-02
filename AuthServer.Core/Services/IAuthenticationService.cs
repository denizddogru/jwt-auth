using AuthServer.Core.Dtos;
using SharedLibrary.Dtos;

namespace AuthServer.Core.Services;


// Direkt olarak API'ye göndereceğimiz kısım, o yüzden response bulunacak.
// Kullanıcıdan EMail, pass alıyoruz ve geriye Token döönüyoruz. 

public interface IAuthenticationService
{

    Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);

    Response<TokenDto> CreateTokenByRefreshTokenAsync(string refreshToken);

    Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);

    Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto loginDto);
}
