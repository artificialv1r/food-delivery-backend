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

    public async Task<PaginatedList<Restaurant>> GetAllRestaurantsPagedAsync(int page, int pageSize)
    {
        int pageIndex = page - 1;
        var query = _context.Restaurants
            .Include(o => o.Owner)
            .ThenInclude(u => u.User);

        var restaurants = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _context.Restaurants.CountAsync();
        PaginatedList<Restaurant> result = new PaginatedList<Restaurant>(restaurants, count, pageIndex, pageSize);
        return result;
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
}