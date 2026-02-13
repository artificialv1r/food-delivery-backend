using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IOwnerRepository _ownerRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository, IOwnerRepository ownerRepository)
    {
        _restaurantRepository = restaurantRepository;
        _ownerRepository = ownerRepository;
    }


    public async Task<List<ShowRestaurantDto>> GetAllRestaurants(int page, int pageSize, string orderDirection)
    {
        var restaurants = await _restaurantRepository.ShowAllRestaurantsAsync(page, pageSize, orderDirection);

        var dtoList = restaurants.Select(r => new ShowRestaurantDto
        {
            Name = r.Name,
            Description = r.Description,
            OwnerFullName = r.Owner.User.Name + " " + r.Owner.User.Surname,
            Meals = r.Meals.ToList(),
        }).ToList();

        return dtoList;
    }
    public async Task<int> CountAllResturants()
    {
        return await _restaurantRepository.CountAllResturantsAsync();
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

    public async Task<Restaurant> UpdateRestaurantAsync(int id, UpdateRestaurantDto dto)
    {
        Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(id);

        if (restaurant == null)
        {
            throw new KeyNotFoundException("Restaurant not found");
        }

        OwnerProfile owner = await _ownerRepository.GetByUserId(dto.OwnerId);

        if (owner == null)
        {
            throw new ArgumentException("Owner not found");
        }

        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.OwnerId = dto.OwnerId;

        return await _restaurantRepository.UpdateRestaurantAsync(restaurant);
    }

    public async Task<bool> DeleteRestaurant(int id)
    {
        Restaurant restaurant = await _restaurantRepository.GetByIdAsync(id);

        if (restaurant == null)
        {
            throw new KeyNotFoundException("Restaurant not found.");
        }

        return await _restaurantRepository.DeleteRestaurantAsync(id);
    }
}