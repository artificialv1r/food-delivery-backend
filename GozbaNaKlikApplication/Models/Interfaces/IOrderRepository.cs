using GozbaNaKlikApplication.DTOs.Orders;

namespace GozbaNaKlikApplication.Models.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<OrderReview> CreateOrderReviewAsync(OrderReview orderReview);
}