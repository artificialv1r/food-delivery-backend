namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<bool> DeleteMeal(int restaurantId, int mealId, int ownerId);
    }
}
