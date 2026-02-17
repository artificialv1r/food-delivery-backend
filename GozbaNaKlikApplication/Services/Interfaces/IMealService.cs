using GozbaNaKlikApplication.DTOs.Meals;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<PaginatedList<ShowMealsDto>> GetMealsForOwner(int ownerId, int page, int pageSize, string orderDirection);

        Task<int> CountMealsForOwner(int ownerId);
    }
}
