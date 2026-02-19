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

        public async Task<PaginatedList<Meal>> GetAllSortedMealsByRestaurantId(
            int restaurantId, int page, int pageSize, MealSortType sortType)
        {
            IQueryable<Meal> meals = _context.Meals
                .Where(m => m.RestaurantId == restaurantId);


            meals = sortType switch
            {
                MealSortType.PriceDesc => meals.OrderByDescending(m => m.Price),
                _ => meals.OrderBy(m => m.Price)
            };

            int pageIndex = page - 1;
            var count = await meals.CountAsync();
            var items = await meals.Skip(pageIndex * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return new PaginatedList<Meal>(items, count, pageIndex, pageSize);
        }
    }
}
