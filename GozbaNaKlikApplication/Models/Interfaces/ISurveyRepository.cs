using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface ISurveyRepository
    {
        Task<List<SurveyAnswer>> CreateSurveyAsync(List<SurveyAnswer> surveyAnswers);
        Task<List<SurveyAnswer>> GetSurveyAnswerAsyncByUserId(int userId);

    }
}