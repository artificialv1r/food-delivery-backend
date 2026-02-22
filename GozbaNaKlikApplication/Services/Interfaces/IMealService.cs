using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<List<ShowMealsDto>> GetAllMealsFromRestaurant(int restaurantId, int ownerId);

    }
}
