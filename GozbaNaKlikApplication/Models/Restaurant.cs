namespace GozbaNaKlikApplication.Models;

public class Restaurant
{
    public int Id { get; set; }
    
    public required string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    public int OwnerId { get; set; }
    public OwnerProfile Owner { get; set; } = null!;
    
    public ICollection<Meal> Meals { get; set; } = new List<Meal>();
}