using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Repositories;

namespace GozbaNaKlikApplication.Services;

public class RestaurantService
{
    private readonly RestaurantRepository _restaurantRepository;
    private readonly OwnerRepository _ownerRepository;

    public RestaurantService(AppDbContext context)
    {
        _restaurantRepository = new RestaurantRepository(context);
        _ownerRepository = new OwnerRepository(context);
    }

    public async Task<Restaurant> CreateRestaurantAsync(AddRestaurantDto dto)
    {
        if (!dto.IsValid())
        {
            throw new ArgumentException("Restaurant name and owner are required.");
        }

        OwnerProfile owner = await _ownerRepository.GetByUserId(dto.OwnerId);

        if (owner == null)
        {
            throw new ArgumentException("Selected owner does not exist.");
        }

        Restaurant restaurant = new Restaurant
        {
            Name = dto.Name,
            OwnerId = dto.OwnerId,
            Description = dto.Description,
        };
        
        return await _restaurantRepository.AddNewRestaurantAsync(restaurant);
    }
}