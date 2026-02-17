using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Controllers;

[Route("api/Restaurants/{restaurantId}/meals")]
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

        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return Unauthorized();
        int userId = int.Parse(userIdClaim);

        try
        {
            ShowMealDto meal = await _mealService.CreateMealAsync(restaurantId, dto, userId);
            return Ok(meal);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (UnauthorizedAccessException e)
        {
            return Forbid();
        }
    }
}