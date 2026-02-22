using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;


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
    }
}
