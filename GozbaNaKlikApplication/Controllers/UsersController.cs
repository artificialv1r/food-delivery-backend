using GozbaNaKlikApplication.Data;
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
}