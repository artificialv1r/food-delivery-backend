namespace GozbaNaKlikApplication.Models;

public class Meal
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
    public ICollection<Allergen>?  MealAllergens { get; set; }
}