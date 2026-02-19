using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
using AutoMapper;
using GozbaNaKlikApplication.Models.Enums;

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

        public async Task<PaginatedList<ShowMealsDto>> GetMealsForOwner(int ownerId, int page, int pageSize, MealSortType sortType)
        {
            var restaurant = await _restaurantRepository.GetRestaurantByOwnerIdAsync(ownerId);

            if (restaurant == null)
                throw new KeyNotFoundException("Restaurant not found for this owner.");

            var meals = await _mealRepository.GetAllSortedMealsByRestaurantId(restaurant.Id, page, pageSize, sortType);

            var mealsDto = _mapper.Map<List<ShowMealsDto>>(meals.Items);

            return new PaginatedList<ShowMealsDto>(mealsDto, meals.Count, meals.PageIndex, pageSize);
        }

    }
}
