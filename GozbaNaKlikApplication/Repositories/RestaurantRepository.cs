using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories;

public class RestaurantRepository
{
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
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