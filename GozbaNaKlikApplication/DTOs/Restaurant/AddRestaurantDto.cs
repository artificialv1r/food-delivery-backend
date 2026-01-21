namespace GozbaNaKlikApplication.DTOs.Restaurant;

public class AddRestaurantDto
{
    public string Name { get; set; }
    public int OwnerId { get; set; }
    public string? Description { get; set; }
    
    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Name) && OwnerId > 0;
    }
}