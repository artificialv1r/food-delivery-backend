using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Models;

public class Order
{
    public int Id { get; set; }
    
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }
    public int? CourierId { get; set; }
    
    // Price
    public decimal MealsPrice { get; set; }
    public decimal DeliveryPrice { get; set; }
    public decimal TotalPrice { get; set; }

    // Timeline and status
    public OrderStatus OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? EstimatedReadyAt { get; set; }
    public DateTime? PickedUpAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
    
    public string DeliveryStreet { get; set; } = string.Empty;
    public string DeliveryCity { get; set; } = string.Empty;
    public string? DeliveryStreetNumber { get; set; }
    public int? DeliveryFloor { get; set; }
    public int? DeliveryApartment { get; set; }
    
    // Relationships
    public ICollection<OrderMeal> MealsOrdered { get; set; } = new List<OrderMeal>();
    public OrderReview? OrderReview { get; set; }
    
    // Navigation
    public Restaurant Restaurant { get; set; }
    public CustomerProfile CustomerProfile { get; set; }
    public CourierProfile? CourierProfile { get; set; }
}