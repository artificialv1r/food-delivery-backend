using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PaginatedList<Meal>> GetMealsByRestaurantIdAsync(int restaurantId, int page, int pageSize, string orderDirection)
        {
            int pageIndex = page - 1;
            var query = _context.Meals
                .Where(m => m.RestaurantId == restaurantId);

            query = orderDirection == "desc"
                ? query.OrderByDescending(m => m.Name)
                : query.OrderBy(m => m.Name);
            
            var meals = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await _context.Meals
                .Where(m => m.RestaurantId == restaurantId)
                .CountAsync();

            var result = new PaginatedList<Meal>(meals, count, pageIndex, pageSize);

            return result;
        }

        public async Task<int> CountMealsByRestaurantAsync(int restaurantId)
        {
            return await _context.Meals
                .Where(m => m.RestaurantId == restaurantId)
                .CountAsync();
        }
    }
}
