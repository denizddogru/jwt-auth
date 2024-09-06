using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp1.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class Invoice : ControllerBase
{

    [HttpGet]

    public IActionResult GetInvoices()
    {

        // veri tabanında name alanına ait istediğimiz bilgiyi alabiliriz artık.

        var userName = HttpContext.User.Identity.Name;

        // veri tabanında Id alanına ait istediğimiz bilgiyi alabiliriz artık.

        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);


        return Ok($"Invoice Operations => Username: {userName} - UserId: {userIdClaim.Value}");

    }
}
