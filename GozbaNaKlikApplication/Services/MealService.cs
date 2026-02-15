using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meal;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Repositories;

namespace GozbaNaKlikApplication.Services
{
    public class MealService: IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public MealService (IMealRepository mealRepository, IRestaurantRepository restaurantRepository)          
        {
            _mealRepository = mealRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<Meal> CreateMealAsync(int restaurantId, CreateMealDto dto)
        {
            if (!dto.IsValid())
            {
                throw new ArgumentException("Meal name and price are required.");
            }

            Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            Meal meal = new Meal
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                RestaurantId = restaurantId,
            };

            return await _mealRepository.CreateMealAsync(meal);
        }
    }
}
