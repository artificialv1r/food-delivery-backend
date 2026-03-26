using System.ComponentModel.DataAnnotations;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.DTOs.Orders;

public class CreateOrderDto
{
    [Required]
    public int RestaurantId { get; set; }
    
    [Required]
    public ICollection<OrderMealDto> MealsOrdered { get; set; } = new List<OrderMealDto>();
    
    [Required]
    public string DeliveryStreet { get; set; }
    [Required]
    public string DeliveryCity { get; set; }
    public string? DeliveryStreetNumber { get; set; }
    public int? DeliveryFloor { get; set; }
    public int? DeliveryApartment { get; set; }
}