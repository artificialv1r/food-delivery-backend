using GozbaNaKlikApplication.DTOs.Orders;

namespace GozbaNaKlikApplication.DTOs.Courier
{
    public class ShowDeliveredOrderDto
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public decimal TotalPrice { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryCity { get; set; }
        public string? DeliveryStreetNumber { get; set; }
        public int? DeliveryFloor { get; set; }
        public int? DeliveryApartment { get; set; }
        public List<ShowOrderMealDto> MealsOrdered { get; set; } = new();
        public DateTime PickedUpAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public int Minutes { get; set; }
    }
}
