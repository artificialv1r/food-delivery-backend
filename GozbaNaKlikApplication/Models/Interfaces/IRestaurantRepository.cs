using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant> AddNewRestaurantAsync(Restaurant restaurant);
        Task<bool> DeleteRestaurantAsync(int id);
        Task<Restaurant> GetByIdAsync(int id);
        Task<PaginatedList<Restaurant>> GetAllRestaurantsPagedAsync(int page, int pageSize);
        Task<Restaurant> UpdateRestaurantAsync(Restaurant restaurant);
        Task<Restaurant?> GetRestaurantByOwnerIdAsync(int ownerId);

    }
}