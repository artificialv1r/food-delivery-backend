using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GozbaNaKlikApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealsController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        public async Task<IActionResult> GetMeals(int page = 1, int pageSize = 5, string orderDirection = "asc")
        {
            if (page < 1 || pageSize < 1)
                return BadRequest("Page and PageSize must be greater than zero.");
            
            try
            {
                int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                var meals = await _mealService.GetMealsForOwner(ownerId, page, pageSize, orderDirection);

                int totalCount = await _mealService.CountMealsForOwner(ownerId);

                return Ok(new
                {
                    ShowMealDto = meals,
                    TotalCount = totalCount
                });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
