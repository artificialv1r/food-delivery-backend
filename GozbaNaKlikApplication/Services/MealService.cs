using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
using AutoMapper;

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

        public async Task<PaginatedList<ShowMealsDto>> GetMealsForOwner(int ownerId, int page, int pageSize, string orderDirection)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByOwnerIdAsync(ownerId);

            if (restaurant == null)
            {
                throw new KeyNotFoundException("Restaurant not found for this owner.");
            }
            var meals = await _mealRepository.GetMealsByRestaurantIdAsync(restaurant.Id, page, pageSize, orderDirection);

            var mealsDto = _mapper.Map<List<ShowMealsDto>>(meals.Items);

            var result = new PaginatedList<ShowMealsDto>(
                mealsDto,
                meals.Count,
                meals.PageIndex,
                pageSize);

            return result;
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
