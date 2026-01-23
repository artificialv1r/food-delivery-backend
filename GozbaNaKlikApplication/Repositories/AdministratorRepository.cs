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

        public async Task<List<UserPreviewDto>> GetPagedAsync(int page, int pageSize, string orderDirection)
        {
            IQueryable<User> query = _context.Users;

            query = orderDirection == "desc"
                ? query.OrderByDescending(u => u.Username)
                : query.OrderBy(u => u.Username);

            query = query.Skip(pageSize * (page - 1))
                         .Take(pageSize);

            var result = query.Select(u => new UserPreviewDto
            {
                Username = u.Username,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email,
                Role = u.Role.ToString()
            });
            return await result.ToListAsync();
        }
        public async Task<int> CountAllUsersAsync()
        {
            return await _context.Users.CountAsync();
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
