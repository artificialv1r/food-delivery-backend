namespace GozbaNaKlikApplication.DTOs.Orders;

public class ShowOrderMealDto
{
    public int MealId { get; set; }
    public string? MealName { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtOrder { get; set; }
}