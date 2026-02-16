using GozbaNaKlikApplication.DTOs.Meal;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<List<ShowMealsDto>> GetMealsForOwner(int ownerId, int page, int pageSize, string orderDirection);

        Task<int> CountMealsForOwner(int ownerId);
    }
}
