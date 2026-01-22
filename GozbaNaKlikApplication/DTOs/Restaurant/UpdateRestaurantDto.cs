namespace GozbaNaKlikApplication.DTOs.Restaurant;

public class UpdateRestaurantDto
{
    public string? Name { get; set; }
    public int OwnerId { get; set; }
    public string? Description { get; set; }
}