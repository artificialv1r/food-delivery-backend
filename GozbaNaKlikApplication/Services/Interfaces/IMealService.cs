namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IMealService
    {
        Task<bool> DeleteMeal(int mealId, int ownerId);
    }
}
