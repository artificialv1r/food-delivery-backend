namespace GozbaNaKlikApplication.DTOs.Meals
{
    public class CreateMealDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && Price > 0;
        }
    }
}
