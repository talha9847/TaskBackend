using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBackend.Interfaces;

namespace TaskBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserInterface _userRepo;

    public UserController(IUserInterface userRepo)
    {
        _userRepo = userRepo;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userRepo.Login(email, password);
        if (user == null)
        {
            return BadRequest(new { message = "Invalid Credentials" });
        }
        return Ok(new { message = "Login Successfully", user.Role });
    }
}

