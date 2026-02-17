namespace GozbaNaKlikApplication.DTOs.Restaurant;

public class RestaurantSearchQuery
{
    public string? Name { get; set; }
    public string? MealName { get; set; }
    // TODO: Kasnije dodati query za pretragu po adresi/lokaciji kada bude implementirana
}