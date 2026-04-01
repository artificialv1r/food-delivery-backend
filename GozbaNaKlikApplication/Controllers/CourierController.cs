using GozbaNaKlikApplication.DTOs.Courier;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GozbaNaKlikApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {
        private ICourierService _courierService;

        public CourierController(ICourierService courierService)
        {
            _courierService = courierService;
        }

        [Authorize(Roles = "Courier")]
        [HttpPost]
        public async Task<ActionResult<CourierWorkingHoursDto>> AddCourireWorkingHoursAsync([FromBody] CourierWorkingHoursDto courireWorkingHoursDto)
        {
            var coruierId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _courierService.AddCourireWorkingHoursAsync(courireWorkingHoursDto, coruierId);
            return Ok(result);
        }
    }
}
