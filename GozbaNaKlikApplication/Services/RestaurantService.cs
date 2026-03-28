using AutoMapper;
using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Meals;
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
    private readonly IMealRepository _mealRepository;
    private readonly IMapper _mapper;


    public RestaurantService(IRestaurantRepository restaurantRepository, IOwnerRepository ownerRepository, IMealRepository mealRepository,IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _ownerRepository = ownerRepository;
        _mealRepository = mealRepository;
        _mapper = mapper;
    }

    public async Task<UpdateRestaurantDto> GetOneRestaurant(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null)
        {
            throw new NotFoundException(id);
        }

        return _mapper.Map<UpdateRestaurantDto>(restaurant);
    }
    
    public async Task<List<ShowRestaurantDto>> GetAllRestaurantsFromOneOwner(int ownerId)
    {
        var restaurants = await _restaurantRepository.GetAllRestaurantsByOwnerIdAsync(ownerId);
        if (!restaurants.Any())
        {
            throw new NotFoundException(ownerId);
        }

        return _mapper.Map<List<ShowRestaurantDto>>(restaurants);

    }

    public async Task<Restaurant> GetRestaurantById(int id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant == null)
        {
            throw new NotFoundException(id);
        }

        return restaurant;
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

    public async Task<PaginatedList<ShowRestaurantDto>> GetFilteredAndSortedRestaurantsPagedAsync(int page, int pageSize, RestaurantSortType sortType,
        RestaurantSearchQuery filter)
    {
        var restaurants = await _restaurantRepository.GetFilteredAndSortedRestaurantsPagedAsync(page, pageSize, sortType, filter);
        var dtos = restaurants.Items
            .Select(_mapper.Map<ShowRestaurantDto>).ToList();
        return new PaginatedList<ShowRestaurantDto>(dtos, restaurants.Count, restaurants.PageIndex, pageSize);
    }

    public async Task<List<MealsDto>> GetAllMealsFromOneRestaurantAsync(int restaurantId)
    {
        var meals = await _mealRepository.GetAllMealsFromOneRestaurantAsync(restaurantId);
        return _mapper.Map<List<MealsDto>>(meals);
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