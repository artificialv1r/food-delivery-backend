using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Restaurant;
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
            .ThenInclude(u => u.User)
            .Include(r => r.Meals);

        var restaurants = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _context.Restaurants.CountAsync();
        PaginatedList<Restaurant> result = new PaginatedList<Restaurant>(restaurants, count, pageIndex, pageSize);
        return result;
    }

    public async Task<PaginatedList<Restaurant>> GetFilteredAndSortedRestaurantsPagedAsync(int page, int pageSize,
        RestaurantSortType sortType, RestaurantSearchQuery filter)
    {
        IQueryable<Restaurant> restaurants = _context.Restaurants
            .Include(r => r.Owner)
            .ThenInclude(u => u.User)
            .Include(r => r.Meals);
        
        restaurants = FilterRestaurants(restaurants, filter);

        restaurants = sortType switch
        {
            RestaurantSortType.NameDesc => restaurants.OrderByDescending(r => r.Name),
            _ => restaurants.OrderBy(r => r.Name)
        };
        
        int pageIndex = page - 1;
        var count = await restaurants.CountAsync();
        var items = await restaurants
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
        PaginatedList<Restaurant> result = new PaginatedList<Restaurant>(items, count, pageIndex, pageSize);
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
            .Include(r => r.Meals)
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
    
    private static IQueryable<Restaurant> FilterRestaurants(IQueryable<Restaurant> restaurants, RestaurantSearchQuery filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            restaurants = restaurants.Where(r=>r.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (!string.IsNullOrWhiteSpace(filter.MealName))
        {
            restaurants = restaurants.Where(r => r.Meals.Any(m => m.Name.ToLower().Contains(filter.MealName.ToLower())));
        }
        
        return restaurants;
    }
}