using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.DTOs.Admin;

public class AdminCreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public UserRole Role { get; set; }
    
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password) && (Role == UserRole.Courier || Role == UserRole.Owner);
    }
}