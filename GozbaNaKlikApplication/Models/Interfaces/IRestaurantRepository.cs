using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> AddNewRestaurantAsync(Restaurant restaurant);
        Task<int> CountAllResturantsAsync();
        Task<bool> DeleteRestaurantAsync(int id);
        Task<Restaurant> GetByIdAsync(int id);
        Task<List<Restaurant>> ShowAllRestaurantsAsync(int page, int pageSize, string orderDirection);
        Task<Restaurant> UpdateRestaurantAsync(Restaurant restaurant);
    }
}