using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        public MealService(IMealRepository mealRepository, IRestaurantRepository restaurantRepository)
        {
            _mealRepository = mealRepository;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<Meal> UpdateMealAsync(int restaurantId, int mealId, UpdateMealDto dto, int userId)
        {
            if (!dto.isValid())
                throw new ArgumentException("Meal name and price are required.");

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new KeyNotFoundException("Restaurant not found.");

            if (restaurant.OwnerId != userId)
                throw new UnauthorizedAccessException("You can only update meals in your own restaurant.");

            Meal meal = await _mealRepository.GetByIdAsync(mealId);
            if (meal == null)
                throw new KeyNotFoundException("Meal not found.");

            if (meal.RestaurantId != restaurantId)
                throw new KeyNotFoundException("Meal does not belong to this restaurant.");

            meal.Name = dto.Name;
            meal.Description = dto.Description;
            meal.Price = dto.Price;

            return await _mealRepository.UpdateMealAsync(meal);
        }
    }
}
