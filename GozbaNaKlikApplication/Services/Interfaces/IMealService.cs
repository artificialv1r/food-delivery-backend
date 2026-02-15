using GozbaNaKlikApplication.DTOs.Meal;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<Meal> CreateMealAsync(int restaurantId, CreateMealDto dto);
    }
}
