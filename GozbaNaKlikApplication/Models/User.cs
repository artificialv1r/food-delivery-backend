namespace GozbaNaKlikApplication.Models;

public class User
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public string Role { get; set; } = "Kupac";
    
    //TODO: Dodati ostala svojstva korisnika
}