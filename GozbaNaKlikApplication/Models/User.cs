using GozbaNaKlikApplication.Models.Enums;
using System.Text.Json.Serialization;

namespace GozbaNaKlikApplication.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }

    [JsonIgnore]
    public UserRole Role { get; set; } = UserRole.Customer;

    [JsonIgnore]
    public AdministratorProfile? AdministratorProfile { get; set; }

    [JsonIgnore]
    public CourierProfile? CourierProfile { get; set; }

    [JsonIgnore]
    public CustomerProfile? CustomerProfile { get; set; }

    [JsonIgnore]
    public OwnerProfile? OwnerProfile { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(PasswordHash);
    }
}