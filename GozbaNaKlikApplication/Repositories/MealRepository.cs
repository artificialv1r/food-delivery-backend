using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly AppDbContext _context;

        public MealRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Meal>> GetAllMealsFromRestaurant(int restaurantId)
        {
            return await _context.Meals
                .Where(m => m.RestaurantId == restaurantId)
                .ToListAsync();
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

        public async Task<Meal> CreateMealAsync(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
            return meal;
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
