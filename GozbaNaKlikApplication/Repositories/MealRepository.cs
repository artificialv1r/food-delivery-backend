using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly AppDbContext _context;

        public MealRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Meal> CreateMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            return meal;
        }
    }
}
