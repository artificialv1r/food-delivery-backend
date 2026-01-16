using System.Security.Claims;
using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(AppDbContext context)
    {
        _userService = new UserService(context);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Registration(User user)
    {
        if (!user.IsValid())
        {
            return BadRequest("Username and password are required.");
        }

        var newUser = await _userService.AddUserAsync(user);

        return Ok(new
        {
            newUser.Id,
            newUser.Username,
            newUser.Email,
            newUser.Name,
            newUser.Surname,
            newUser.Role
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        if (!request.IsValid())
        {
            return BadRequest("Username or password is incorrect");
        }
        try
        {
            User user = await _userService.Login(request.Username, request.Password);

            if (user == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            var token = _userService.GenerateJwtToken(user);
            return Ok(new
            {
                user.Id,
                user.Username,
                role = user.Role,
                token
            });
        }
        catch (Exception e)
        {
            return Unauthorized("Username or password is incorrect" + e.Message);
        }
    }


    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            User.Identity.Name,
            Role = User.FindFirst(ClaimTypes.Role)?.Value
        });
    }

}