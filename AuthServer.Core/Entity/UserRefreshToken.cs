namespace AuthServer.Core.Entity;
public class UserRefreshToken
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpirationDate { get; set; }
}
