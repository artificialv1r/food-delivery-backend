using GozbaNaKlikApplication.DTOs.Orders;

namespace GozbaNaKlikApplication.Models.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order);
    Task<Order?> GetActiveCourierOrder(int courierId);
    Task<Order> UpdateOrder(Order order);
    Task<List<Order>> GetPendingOrdersByRestaurant(int restaurantId);
    Task<Order?> GetOrderById(int orderId);
    Task<List<Order>> GetAcceptedOrdersByRestaurant(int restaurantId);
    Task<List<Order>> GetCanceledOrdersByRestaurant(int restaurantId);
    Task<Order> GetOrderByIdAsync(int orderId);
}