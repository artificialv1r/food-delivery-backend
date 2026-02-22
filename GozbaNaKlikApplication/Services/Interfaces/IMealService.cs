using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces﻿
{
    public interface IMealService
    {
        Task<bool> DeleteMeal(int restaurantId, int mealId, int ownerId);    
        Task<ShowMealDto> CreateMealAsync(int restaurantId, CreateMealDto dto, int userId);
        Task<ShowMealDto> UpdateMealAsync(int restaurantId, int mealId, UpdateMealDto dto, int userId);
    }
}
