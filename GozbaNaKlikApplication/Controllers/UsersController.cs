using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Registration(User user)
    {
        if (!user.IsValid())
        {
            return BadRequest("Username and password are required.");
        }

        if (user.Role != UserRole.Customer)
        {
            throw new Exception("Invalid role for user registration. ");
        }
        var newUser = await _userService.AddUserAsync(user);

        return Ok(new
        {
            id = newUser.Id,
            message = "User successfully registered."
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

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<ActionResult<PaginatedList<UserPreviewDto>>> GetAllUsersPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        return Ok(await _userService.GetAllUsersPagedAsync(page, pageSize));
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