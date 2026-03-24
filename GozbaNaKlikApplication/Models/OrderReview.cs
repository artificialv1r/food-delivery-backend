namespace GozbaNaKlikApplication.Models;

public class OrderReview
{
    public int Id { get; set; }
    
    public int RestaurantGrade { get; set; }
    public string? RestaurantComment { get; set; }
    
    public int CourierGrade { get; set; }
    public string? CourierComment { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public int CustomerId { get; set; }
    public CustomerProfile Customer { get; set; }
    
    public int CourierId { get; set; }
    public CourierProfile Courier { get; set; }
    
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}