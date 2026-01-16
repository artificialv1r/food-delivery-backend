using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }

    public UserRole Role { get; set; } = UserRole.Customer;

    public AdministratorProfile? AdministratorProfile { get; set; }
    public CourierProfile? CourierProfile { get; set; }
    public CustomerProfile? CustomerProfile { get; set; }
    public OwnerProfile? OwnerProfile { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(PasswordHash);
    }
}