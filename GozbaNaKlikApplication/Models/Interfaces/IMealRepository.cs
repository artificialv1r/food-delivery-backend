namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IMealRepository
    {
        Task<Meal> CreateMealAsync(Meal meal);

    }
}
