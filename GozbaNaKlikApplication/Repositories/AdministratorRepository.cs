using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories
{
    public class AdministratorRepository
    {
        private AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurant>> ShowAllRestaurantsAsync(int page, int pageSize, string orderDirection)
        {
            IQueryable<Restaurant> query = _context.Restaurants;

            query = orderDirection == "desc"
                ? query.OrderByDescending(r => r.Name)
                : query.OrderBy(r => r.Name);

            query = query.Skip(pageSize * (page - 1))
                         .Take(pageSize);

            return await query.ToListAsync();   
        }

        public async Task<int> CountAllResturantsAsync()
        {
            return await _context.Restaurants.CountAsync();
        }
    }
}
