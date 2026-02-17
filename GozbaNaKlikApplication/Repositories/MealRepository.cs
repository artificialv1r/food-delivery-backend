using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly AppDbContext _context;

        public MealRepository (AppDbContext context)
        {
            _context = context;
        }

        public async Task<Meal?> GetByIdAsync(int id)
        {
            return await _context.Meals.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Meal> UpdateMealAsync(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
            return meal;
        }
    }
}
