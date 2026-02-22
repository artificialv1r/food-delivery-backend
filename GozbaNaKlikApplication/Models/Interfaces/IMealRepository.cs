namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> GetMealByIdAsync(int mealId);
        Task<bool> DeleteMealAsync(int mealId);
        Task<Meal> CreateMealAsync(Meal meal);
        Task<Meal> UpdateMealAsync(Meal meal);
        Task<Meal?> GetByIdAsync(int id);
    }
}
