using Microsoft.EntityFrameworkCore;
using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;

namespace GozbaNaKlikApplication.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly AppDbContext _context;

        public MealRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Meal> GetMealByIdAsync(int mealId)
        {
            return await _context.Meals.FindAsync(mealId);
        }

        public async Task<bool> DeleteMealAsync(int mealId)
        {
            var meal = await _context.Meals.FindAsync(mealId);
            if (meal == null)
                return false;

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
