using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.DTOs.Meals
{
    public class MealsDto
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<Allergen>? Allergens { get; set; }
    }
}
