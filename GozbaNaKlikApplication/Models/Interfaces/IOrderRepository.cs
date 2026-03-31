using GozbaNaKlikApplication.DTOs.Orders;

namespace GozbaNaKlikApplication.Models.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order);
    Task<Order> UpdateOrder(Order order);
    Task<List<Order>> GetPendingOrdersByRestaurant(int restaurantId);
    Task<Order?> GetOrderById(int orderId);
}