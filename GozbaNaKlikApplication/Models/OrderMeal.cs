namespace GozbaNaKlikApplication.Models;

public class OrderMeal
{
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public int MealId { get; set; }
    public Meal Meal { get; set; }
    
    public int Quantity { get; set; }
    public decimal PriceAtOrder { get; set; }
}