using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiniApp1.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class StockController : ControllerBase
{

    [HttpGet]
    public IActionResult GetStock()
    {

        // veri tabanında name alanına ait istediğimiz bilgiyi alabiliriz artık.

        var userName = HttpContext.User.Identity.Name;

        // veri tabanında Id alanına ait istediğimiz bilgiyi alabiliriz artık.

        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);


        return Ok($"Stock Operations => Username: {userName} - UserId: {userIdClaim.Value}");

    }
}
