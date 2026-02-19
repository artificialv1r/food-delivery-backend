using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Models.Interfaces

{
    public interface IMealRepository
    {
        Task<PaginatedList<Meal>> GetAllSortedMealsByRestaurantId(
            int restaurantId, int page, int pageSize, MealSortType sortType);

    }
}
