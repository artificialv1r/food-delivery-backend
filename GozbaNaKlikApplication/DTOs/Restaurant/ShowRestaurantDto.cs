using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.DTOs.Restaurant
{
    public class ShowRestaurantDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string OwnerFullName { get; set; }
        public List<GozbaNaKlikApplication.Models.Meal> Meals { get; set; } = new List<GozbaNaKlikApplication.Models.Meal>();
    }
}
