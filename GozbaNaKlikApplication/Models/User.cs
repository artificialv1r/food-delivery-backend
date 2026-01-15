using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Models;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    
    public UserRole Role { get; set; }
    //TODO: Dodati ostala svojstva korisnika
}