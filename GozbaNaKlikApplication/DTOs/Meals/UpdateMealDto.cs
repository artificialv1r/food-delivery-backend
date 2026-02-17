namespace GozbaNaKlikApplication.DTOs.Meals
{
    public class UpdateMealDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public bool isValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && Price > 0;
        }
    }
}
