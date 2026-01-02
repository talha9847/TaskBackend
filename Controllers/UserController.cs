using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBackend.Interfaces;
using TaskBackend.Models;
using TaskBackend.Services;

namespace TaskBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserInterface _userRepo;
    private readonly JwtService _jwtService;

    public UserController(IUserInterface userRepo, JwtService jwtService)
    {
        _userRepo = userRepo;
        _jwtService = jwtService;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginModel login)
    {
        var user = await _userRepo.Login(login.Email ?? "", login.Password ?? "");
        if (user == null)
        {
            return BadRequest(new { message = "Invalid Credentials" });
        }
        var token = await _jwtService.JwtGeneration(user.Id, user.Role ?? "");
        return Ok(new { message = "Login Successfully", user.Role, token });
    }
}

