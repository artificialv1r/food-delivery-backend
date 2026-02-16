using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.DTOs.Restaurant
{
    public class ShowRestaurantDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string OwnerUserName { get; set; }
        public List<Meal> Meals { get; set; } = new List<Meal>();
    }
}
