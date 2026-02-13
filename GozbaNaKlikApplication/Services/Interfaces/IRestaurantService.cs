using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<int> CountAllResturants();
        Task<Restaurant> CreateRestaurantAsync(AddRestaurantDto dto);
        Task<bool> DeleteRestaurant(int id);
        Task<List<ShowRestaurantDto>> GetAllRestaurants(int page, int pageSize, string orderDirection);
        Task<Restaurant> UpdateRestaurantAsync(int id, UpdateRestaurantDto dto);
    }
}