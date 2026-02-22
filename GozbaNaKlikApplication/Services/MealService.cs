using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
using AutoMapper;
using GozbaNaKlikApplication.Models.Enums;
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
        }
    }
}
