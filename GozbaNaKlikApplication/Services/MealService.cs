using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.Repositories;


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
        public async Task<bool> DeleteMeal(int restaurantId, int mealId, int ownerId)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByOwnerIdAsync(ownerId);
            if (restaurant == null)
                throw new KeyNotFoundException("No restaurant for this owner.");

            if (restaurant.OwnerId != ownerId)
                throw new UnauthorizedAccessException("You can only delete meals from your own restaurant.");

            var meal = await _mealRepository.GetMealByIdAsync(mealId);
            if (meal == null)
                throw new KeyNotFoundException("MEal not found.");

            if (meal.RestaurantId != restaurant.Id)
                throw new UnauthorizedAccessException("You are not authorized to delete this.");

            return await _mealRepository.DeleteMealAsync(mealId);
        }
    }
}
