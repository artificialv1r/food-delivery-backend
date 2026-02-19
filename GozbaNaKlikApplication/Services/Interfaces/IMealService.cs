using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<ShowMealDto> CreateMealAsync(int restaurantId, CreateMealDto dto, int userId);
        Task<Meal> UpdateMealAsync(int restaurantId, int mealId, UpdateMealDto dto, int userId);
    }
}
