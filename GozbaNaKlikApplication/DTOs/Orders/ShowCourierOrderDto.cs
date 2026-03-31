using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.DTOs.Orders
{
    public class ShowCourierOrderDto
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryCity { get; set; }
        public string? DeliveryStreetNumber { get; set; }
        public int? DeliveryFloor { get; set; }
        public int? DeliveryApartment { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerSurname { get; set; }
        public string? CustomerEmail { get; set; }
        public List<CourierOrderMealDto> Meals { get; set; }

    }

    public class CourierOrderMealDto
    {
        public string MealName { get; set; }
        public int Quantity { get; set; }
    }
}
