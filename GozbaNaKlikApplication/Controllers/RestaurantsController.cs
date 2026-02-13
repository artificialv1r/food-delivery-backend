using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Services;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }


    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants(int page = 1, int pageSize = 5, string orderDirection = "asc")
    {
        if (page < 1 || pageSize < 1) { return BadRequest("Page and PageSize must be greater then zero."); }

        try
        {
            var restaurants = await _restaurantService.GetAllRestaurants(page, pageSize, orderDirection);
            int totalCount = await _restaurantService.CountAllResturants();

            return Ok(new
            {
                ShowRestaurantDto = restaurants,
                TotalCount = totalCount
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("You must be logged in as an Administrator to perform this action.");
        }
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost("restaurant")]
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
    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        try
        {
            await _restaurantService.DeleteRestaurant(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}