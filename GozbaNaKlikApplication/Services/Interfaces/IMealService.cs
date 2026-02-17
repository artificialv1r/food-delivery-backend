using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<Meal> UpdateMealAsync(int restaurantId, int mealId, UpdateMealDto dto, int userId);
    }
}