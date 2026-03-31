using System.ComponentModel.DataAnnotations;

namespace GozbaNaKlikApplication.DTOs.Orders;

public class AcceptOrderDto : IValidatableObject
{
    [Required]
    public DateTime EstimatedReadyAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EstimatedReadyAt < DateTime.UtcNow.AddMinutes(5))
        {
            yield return new ValidationResult(
                "Estimated ready time must be at least 5 minutes in the future.",
                [nameof(EstimatedReadyAt)]
            );
        }
    }
}