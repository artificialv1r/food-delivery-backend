using AutoMapper;
using GozbaNaKlikApplication.DTOs.Survey;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;

        public SurveyService(ISurveyRepository surveyRepository, IMapper mapper)
        {
            _surveyRepository = surveyRepository;
            _mapper = mapper;
        }

        public async Task<List<SurveyAnswerDto>> CreateSurveyAsync(List<SurveyAnswerDto> dto, int userId)
        {
            var existingAnswers = await _surveyRepository.GetSurveyAnswerAsyncByUserId(userId);
            if (existingAnswers.Any())
            {
                throw new BadRequestException("You can only live rating one.");
            }
            if (dto.Count != 5)
            {
                throw new BadRequestException("Survey must have exactly 5 answers.");
            }

            var surveyAnswer = dto.Select(a => new SurveyAnswer
            {
                UserId = userId,
                QuestionId = a.QuestionId,
                Rating = a.Rating,
                Comment = a.Comment
            }).ToList();

            var savedAnswers = await _surveyRepository.CreateSurveyAsync(surveyAnswer);
            return savedAnswers.Select(a => _mapper.Map<SurveyAnswerDto>(a)).ToList();
        }
    }
}
