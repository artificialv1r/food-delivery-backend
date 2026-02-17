using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GozbaNaKlikApplication.Controllers
{
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
        [HttpPut("{mealId}")]
        public async Task<IActionResult> UpdateMeal(int restaurantId, int mealId, [FromBody] UpdateMealDto dto)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();
            int userId = int.Parse(userIdClaim);
            try
            {
                Meal meal = await _mealService.UpdateMealAsync(restaurantId, mealId, dto, userId);
                return Ok(new
                {
                    meal.Id,
                    meal.Name,
                    meal.Description,
                    meal.Price,
                    meal.RestaurantId
                });
            }
            catch (ArgumentException e) { return BadRequest(e.Message); }
            catch (KeyNotFoundException e) { return NotFound(e.Message); }
            catch (UnauthorizedAccessException e) { return Forbid(); }
        }
    }
}
