using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;

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
}