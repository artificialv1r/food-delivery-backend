using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories
{
    public class SurveyRepository : ISurveyRepository
    {
        private AppDbContext _context;

        public SurveyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SurveyAnswer>> CreateSurveyAsync(List<SurveyAnswer> surveyAnswers)
        {

            _context.SurveyAnswers.AddRange(surveyAnswers);
            await _context.SaveChangesAsync();

            var ids = surveyAnswers.Select(a => a.Id).ToList();
            var savedAnswers = await _context.SurveyAnswers
                .Where(a => ids.Contains(a.Id))
                .Include(a => a.User)
                .Include(a => a.Question)
                .ToListAsync();
            return savedAnswers;
        }
        public async Task<List<SurveyAnswer>> GetSurveyAnswerAsyncByUserId(int userId)
        {
            return await _context.SurveyAnswers
                .Where(s => s.UserId == userId)
                .Include(s => s.User)
                .Include(s => s.Question)
                .ToListAsync();
        }
    }
}
