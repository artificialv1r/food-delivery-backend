using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Admin;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdministratorController:ControllerBase
{
    private readonly AdministratorService _administratorService;
    private readonly RestaurantService _restaurantService;

    public AdministratorController(AppDbContext context)
    {
        _administratorService = new AdministratorService(context);
        _restaurantService = new RestaurantService(context);
    }
    
    [Authorize (Roles =  "Administrator")]
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

    [Authorize (Roles =  "Administrator")]
    [HttpPost("restaurants")]
    public async Task<IActionResult> CreateNewRestaurant([FromBody] AddRestaurantDto dto)
    {
        try
        {
            Restaurant restaurant = await _restaurantService.CreateRestaurantAsync(dto);
            return Ok(new
            {
                restaurant.Id,
                restaurant.Name,
                restaurant.Description,
                restaurant.OwnerId
            });
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("restaurants/{id}")]
    public async Task<IActionResult> UpdateRestaurant(int id, AddRestaurantDto dto)
    {
        try
        {
            Restaurant updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(id, dto);

            return Ok(new
            {
                updatedRestaurant.Id,
                updatedRestaurant.Name,
                updatedRestaurant.Description,
                updatedRestaurant.OwnerId
            });
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}