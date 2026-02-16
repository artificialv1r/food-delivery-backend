using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
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

        public async Task<List<Meal>> GetMealsByRestaurantIdAsync(int restaurantId, int page, int pageSize, string orderDirection)
        {
            IQueryable<Meal> query = _context.Meals
                .Where(m => m.RestaurantId == restaurantId);

            query = orderDirection == "desc"
                ? query.OrderByDescending(m => m.Name)
                : query.OrderBy(m => m.Name);

            query = query.Skip(pageSize * (page - 1)).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<int> CountMealsByRestaurantAsync(int restaurantId)
        {
            return await _context.Meals
                .Where(m => m.RestaurantId == restaurantId)
                .CountAsync();
        }
    }
}
