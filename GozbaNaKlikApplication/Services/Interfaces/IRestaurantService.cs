using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<Restaurant> CreateRestaurantAsync(AddRestaurantDto dto);
        Task<bool> DeleteRestaurant(int id);
        Task<PaginatedList<ShowRestaurantDto>> GetAllRestaurantsAsync(int page, int pageSize);
        Task<Restaurant> UpdateRestaurantAsync(int id, UpdateRestaurantDto dto);
    }
}