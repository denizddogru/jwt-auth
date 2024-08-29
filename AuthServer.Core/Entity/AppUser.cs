using Microsoft.AspNetCore.Identity;

namespace AuthServer.Core.Entity;
public class AppUser : IdentityUser
{
    public string City { get; set; }
}
