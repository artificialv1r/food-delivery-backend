using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Models.Interfaces

{
    public interface IMealRepository
    {
        Task<List<Meal>> GetAllMealsFromRestaurant(int restaurantId);

    }
}
