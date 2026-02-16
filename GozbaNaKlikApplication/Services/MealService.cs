using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meal;

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

        public async Task<List<ShowMealsDto>> GetMealsForOwner(int ownerId, int page, int pageSize, string orderDirection)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByOwnerIdAsync(ownerId);


            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found for this owner.");
            }
            var meals = await _mealRepository.GetMealsByRestaurantIdAsync(restaurant.Id, page, pageSize, orderDirection);

            return meals.Select(meal => new ShowMealsDto
            {
                Name = meal.Name,
                Description = meal.Description,
                Price = meal.Price
            }).ToList();
        }

        public async Task<int> CountMealsForOwner(int ownerId)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByOwnerIdAsync(ownerId);

            if (restaurant == null)
                throw new KeyNotFoundException("Restaurant not found for this owner.");

            return await _mealRepository.CountMealsByRestaurantAsync(restaurant.Id);
        }
    }
}
