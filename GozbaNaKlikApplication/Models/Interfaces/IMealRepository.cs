using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Models.Interfaces

{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllMealsFromRestaurant(int restaurantId);
      
        Task<Meal> GetMealByIdAsync(int mealId);
        Task<bool> DeleteMealAsync(int mealId);
        Task<Meal> CreateMealAsync(Meal meal);
        Task<Meal> UpdateMealAsync(Meal meal);
        Task<Meal?> GetByIdAsync(int id);
        Task<List<Meal>> GetAllMealsFromOneRestaurantAsync(int restaruantId);
    }
}
