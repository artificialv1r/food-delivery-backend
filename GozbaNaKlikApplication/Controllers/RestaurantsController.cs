using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Services;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    private readonly IMealService _mealService;

    public RestaurantsController(IRestaurantService restaurantService,IMealService mealService)
    {
        _restaurantService = restaurantService;
        _mealService = mealService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UpdateRestaurantDto>> GetRestaurantByIdAsync(int id)
    {
        return Ok(await _restaurantService.GetOneRestaurant(id));  
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ShowRestaurantDto>>> GetAllRestaurantsPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        return Ok(await _restaurantService.GetAllRestaurantsPagedAsync(page, pageSize));
    }

    [Authorize(Roles = "Customer")]
    [HttpGet("{restaurantId}/menu")]
    public async Task<ActionResult<List<MealsDto>>> GetAllMealsFromOneRestaurantAsync([FromRoute] int restaurantId)
    {
        var restaurant = await _restaurantService.GetRestaurantById(restaurantId);
        if(restaurant == null)
        {
            throw new NotFoundException(restaurantId);
        }
        return Ok(await _restaurantService.GetAllMealsFromOneRestaurantAsync(restaurantId));
    }

    [Authorize(Roles = "Owner")]
    [HttpGet("{restaurantId}/meals")]
    public async Task<IActionResult> GetAllMealsFromRestaurant([FromRoute] int restaurantId)
    {
        try
        {
            int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return Ok(await _mealService.GetAllMealsFromRestaurant(restaurantId, ownerId));
        }
        catch (UnauthorizedAccessException e)
        {
            return StatusCode(403, e.Message);
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

    [HttpGet("filter")]
    public async Task<ActionResult<PaginatedList<ShowRestaurantDto>>> GetFilteredAndSortedRestaurantsPagedAsync( [FromQuery] RestaurantSortType sortType, [FromQuery] RestaurantSearchQuery filter, [FromQuery] int page=1,[FromQuery] int pageSize = 10)
    {
        return Ok(await _restaurantService.GetFilteredAndSortedRestaurantsPagedAsync(page, pageSize, sortType, filter));
    }
}