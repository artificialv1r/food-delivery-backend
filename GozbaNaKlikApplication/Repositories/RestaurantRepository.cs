using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Restaurant>> ShowAllRestaurantsAsync(int page, int pageSize, string orderDirection)
    {
        IQueryable<Restaurant> query = _context.Restaurants
            .Include(o => o.Owner)
            .ThenInclude(u => u.User);

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
    public async Task<Restaurant> AddNewRestaurantAsync(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return restaurant;
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        return await _context.Restaurants
            .Include(r => r.Owner)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Restaurant> UpdateRestaurantAsync(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
        return restaurant;
    }

    public async Task<bool> DeleteRestaurantAsync(int id)
    {
        Restaurant restraurant = await _context.Restaurants.FindAsync(id);
        if (restraurant == null)
        {
            return false;
        }
        _context.Remove(restraurant);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<Restaurant?> GetRestaurantByOwnerIdAsync(int ownerId)
    {
        return await _context.Restaurants
            .FirstOrDefaultAsync(r => r.OwnerId == ownerId);
    }
}