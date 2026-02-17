using System.Security.Claims;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            try
            {
                int ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                await _mealService.DeleteMeal(id, ownerId);

                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                return Forbid(e.Message);
            }
        }
    }
}
