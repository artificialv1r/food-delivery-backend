using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<PaginatedList<ShowMealsDto>> GetMealsForOwner(int ownerId, int page, int pageSize, MealSortType sortType);

    }
}
