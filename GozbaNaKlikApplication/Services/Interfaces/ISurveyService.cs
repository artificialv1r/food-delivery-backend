using GozbaNaKlikApplication.DTOs.Survey;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface ISurveyService
    {
        Task<List<SurveyAnswerDto>> CreateSurveyAsync(List<SurveyAnswerDto> dto, int userId);
    }
}