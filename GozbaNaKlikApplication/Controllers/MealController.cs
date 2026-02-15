using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meal;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/restaurants/{restaurantId}/meals")]
[ApiController]
public class MealsController : ControllerBase
{
    private readonly IMealService _mealService;

    public MealsController(IMealService mealService)
    {
        _mealService = mealService;
    }

    [Authorize(Roles = "Owner")]
    [HttpPost]
    public async Task<IActionResult> CreateMeal(int restaurantId, [FromBody] CreateMealDto dto)
    {
        try
        {
            Meal meal = await _mealService.CreateMealAsync(restaurantId, dto);
            return Ok(new
            {
                meal.Id,
                meal.Name,
                meal.Description,
                meal.Price,
                meal.RestaurantId
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