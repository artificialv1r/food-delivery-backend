namespace GozbaNaKlikApplication.Models.Interfaces

{
    public interface IMealRepository
    {
        Task<PaginatedList<Meal>> GetMealsByRestaurantIdAsync(int restaurantId, int page, int pageSize, string orderDirection);

        Task<int> CountMealsByRestaurantAsync(int restaurantId);
    }
}
