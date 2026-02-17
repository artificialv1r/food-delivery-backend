namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> UpdateMealAsync(Meal meal);
        Task<Meal?> GetByIdAsync(int id);
    }
}
