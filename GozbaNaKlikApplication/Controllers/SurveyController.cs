using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.DTOs.Survey;
using GozbaNaKlikApplication.Services;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GozbaNaKlikApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;

        public SurveyController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List<SurveyAnswerDto>>> CreateSurveyAsync([FromBody] List<SurveyAnswerDto> dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return Ok(await _surveyService.CreateSurveyAsync(dto, userId));
        }
    }
}
