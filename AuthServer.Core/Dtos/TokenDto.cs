namespace AuthServer.Core.Dtos;
public class TokenDto
{
    public string AccessToken { get; set; }

    public DateTime AccessTokenExpirationDate { get; set; }

    public string RefreshToken { get; set; }

    public DateTime RefreshTokenExpirationDate { get; set; }
}
