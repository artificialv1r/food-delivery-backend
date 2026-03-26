using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Orders;

public class OrderMealDto
{
    [Required]
    public int MealId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
}