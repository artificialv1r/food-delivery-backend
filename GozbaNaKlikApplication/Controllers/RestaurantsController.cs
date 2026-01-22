using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly RestaurantService _restaurantService;

    public RestaurantsController(AppDbContext context)
    {
        _restaurantService = new RestaurantService(context);
    }
    
    [Authorize(Roles = "Administrator,Owner")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant(int id, UpdateRestaurantDto dto)
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