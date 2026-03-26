using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<Restaurant> CreateRestaurantAsync(AddRestaurantDto dto);
        Task<bool> DeleteRestaurant(int id);
        Task<PaginatedList<ShowRestaurantDto>> GetAllRestaurantsPagedAsync(int page, int pageSize);
        Task<Restaurant> UpdateRestaurantAsync(int id, UpdateRestaurantDto dto);
        Task<PaginatedList<ShowRestaurantDto>> GetFilteredAndSortedRestaurantsPagedAsync(int page, int pageSize, RestaurantSortType sortType, RestaurantSearchQuery filter);
        Task<Restaurant> GetRestaurantById(int id);
        Task<UpdateRestaurantDto> GetOneRestaurant(int id);
        Task<List<MealsDto>> GetAllMealsFromOneRestaurantAsync(int restaurantId);

    }
}