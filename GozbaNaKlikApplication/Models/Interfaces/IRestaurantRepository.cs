using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> AddNewRestaurantAsync(Restaurant restaurant);
        Task<bool> DeleteRestaurantAsync(int id);
        Task<Restaurant> GetByIdAsync(int id);
        Task<PaginatedList<Restaurant>> ShowAllRestaurantsAsync(int page, int pageSize);
        Task<Restaurant> UpdateRestaurantAsync(Restaurant restaurant);
    }
}