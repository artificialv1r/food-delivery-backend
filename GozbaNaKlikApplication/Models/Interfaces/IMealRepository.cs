namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> GetMealByIdAsync(int mealId);
        Task<bool> DeleteMealAsync(int mealId);
    }
}
