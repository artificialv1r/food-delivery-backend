using AutoMapper;
using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IMapper _mapper;


    public RestaurantService(IRestaurantRepository restaurantRepository, IOwnerRepository ownerRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _ownerRepository = ownerRepository;
        _mapper = mapper;
    }


    public async Task<PaginatedList<ShowRestaurantDto>> GetAllRestaurantsPagedAsync(int page, int pageSize)
    {
        var restaurants = await _restaurantRepository.GetAllRestaurantsPagedAsync(page, pageSize);
        var restaurantDto = _mapper.Map<List<ShowRestaurantDto>>(restaurants.Items);

        var result = new PaginatedList<ShowRestaurantDto>(
            restaurantDto,
            restaurants.Count,
            restaurants.PageIndex,
            pageSize);
        return result;
    }
    
    public async Task<Restaurant> CreateRestaurantAsync(AddRestaurantDto dto)
    {
        if (!dto.IsValid())
        {
            throw new BadRequestException("Restaurant name and owner are required.");
        }

        OwnerProfile owner = await _ownerRepository.GetByUserId(dto.OwnerId);

        if (owner == null)
        {
            throw new BadRequestException("Selected owner does not exist.");
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
            throw new NotFoundException(id);
        }

        OwnerProfile owner = await _ownerRepository.GetByUserId(dto.OwnerId);

        if (owner == null)
        {
            throw new NotFoundException(dto.OwnerId);
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
            throw new NotFoundException(id);
        }

        return await _restaurantRepository.DeleteRestaurantAsync(id);
    }
}