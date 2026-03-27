using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Orders;

public class AcceptOrderDto
{
    [Required]
    public DateTime EstimatedReadyAt { get; set; }
}