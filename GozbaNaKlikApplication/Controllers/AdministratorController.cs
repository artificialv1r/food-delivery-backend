using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdministratorController : ControllerBase
{
    private readonly AdministratorService _administratorService;

    public AdministratorController(AppDbContext context)
    {
        _administratorService = new AdministratorService(context);
    }


    [Authorize(Roles = "Administrator")]
    [HttpPost("users")]
    public async Task<IActionResult> RegisterNewUser(User user)
    {
        if (!user.IsValid())
        {
            return BadRequest("Username and password are required.");
        }

        if (!Enum.IsDefined(typeof(UserRole), user.Role))
        {
            return BadRequest($"Invalid role value: {(int)user.Role}");
        }

        if (user.Role != UserRole.Owner && user.Role != UserRole.Courier)
        {
            return BadRequest("Only Owner and Courier roles can be registered by administrators.");
        }

        try
        {
            var createdUser = await _administratorService.RegisterNewUser(user);

            return Ok(new
            {
                createdUser.Id,
                createdUser.Username,
                createdUser.Role
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized("You must be logged in as an Administrator to perform this action.");
        }
    }
}