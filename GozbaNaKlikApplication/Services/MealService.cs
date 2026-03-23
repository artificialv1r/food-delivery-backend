using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Repositories;
using AutoMapper;
using System.Linq;

namespace GozbaNaKlikApplication.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public MealService(IMealRepository mealRepository, IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            _mealRepository = mealRepository;
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
        }

        public async Task<List<ShowMealsDto>> GetAllMealsFromRestaurant(int restaurantId, int ownerId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);

            if (restaurant == null || restaurant.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You are not authorized to view meals from this restaurant.");

            var meals = await _mealRepository.GetAllMealsFromRestaurant(restaurantId);

            var mealsDto = meals.Select(m => _mapper.Map<ShowMealsDto>(m))
                                .ToList();

            return mealsDto;
        public async Task<bool> DeleteMeal(int restaurantId, int mealId, int ownerId)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
                throw new KeyNotFoundException("No restaurant for this owner.");

            if (restaurant.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You can only delete meals from your own restaurant.");

            var meal = await _mealRepository.GetMealByIdAsync(mealId);
            if (meal == null)
                throw new KeyNotFoundException("Meal not found.");

            if (meal.RestaurantId != restaurant.Id)
                throw new KeyNotFoundException("You are not authorized to delete this.");

            return await _mealRepository.DeleteMealAsync(mealId);
        }
        public async Task<ShowMealDto> CreateMealAsync(int restaurantId, CreateMealDto dto, int userId)
        {
            if (!dto.IsValid())
            {
                throw new ArgumentException("Meal name and price are required.");
            }

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }
            if (restaurant.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You can only add meals to your own restaurant.");
            }

            Meal meal = _mapper.Map<Meal>(dto);
            meal.RestaurantId = restaurantId;
            Meal createdMeal = await _mealRepository.CreateMealAsync(meal);
            return _mapper.Map<ShowMealDto>(createdMeal);
        }

        public async Task<ShowMealDto> UpdateMealAsync(int restaurantId, int mealId, UpdateMealDto dto, int userId)
        {

            Restaurant restaurant = await _restaurantRepository.GetByIdAsync(restaurantId);
            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found.");
            }

            if (restaurant.OwnerId != userId)
            {
                throw new UnauthorizedAccessException("You can only update meals in your own restaurant.");
            }

            Meal meal = await _mealRepository.GetByIdAsync(mealId);
            if (meal == null)
            {
                throw new KeyNotFoundException("Meal not found.");
            }

            if (meal.RestaurantId != restaurantId)
            {
                throw new KeyNotFoundException("Meal does not belong to this restaurant.");
            }

            _mapper.Map(dto, meal);

            Meal updatedMeal = await _mealRepository.UpdateMealAsync(meal);
            return _mapper.Map<ShowMealDto>(updatedMeal);

        }
    }
}
