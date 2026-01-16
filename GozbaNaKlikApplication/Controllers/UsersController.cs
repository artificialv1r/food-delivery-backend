using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController:ControllerBase
{
    private readonly UserService _userService;

    public UsersController(AppDbContext context)
    {
        _userService = new UserService(context);
    }

    //TODO: Definisati API endpoint-e (npr. [HttpGet], [HttpPost], [HttpPut], [HttpDelete])

    [HttpPost]
    public async Task<IActionResult> AddNewUserAsync(User user)
    {
        return Ok(await _userService.AddNewUserAsync(user));

    }
}