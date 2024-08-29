namespace AuthServer.Core.Dtos;
public class ClientTokenDto
{
    public string AccessToken { get; set; }

    public DateTime AccessTokenExpirationDate { get; set; }


}
