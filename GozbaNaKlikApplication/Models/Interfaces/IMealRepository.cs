namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> CreateMealAsync(Meal meal);
        Task<Meal> UpdateMealAsync(Meal meal);
        Task<Meal?> GetByIdAsync(int id);
    }
}
