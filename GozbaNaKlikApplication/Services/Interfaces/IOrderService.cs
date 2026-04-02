using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.Models.Enums;

namespace GozbaNaKlikApplication.Services.Interfaces;

public interface IOrderService
{
    Task<ShowOrderDto> CreateOrder(CreateOrderDto orderDto, int customerId);
    Task<ShowCourierOrderDto?> GetActiveCourierOrder(int courierId);
    Task<List<ShowOrderDto>> GetPendingOrdersByRestaurant(int restaurantId, int requestingUserId, string role);
    Task<ShowOrderDto> AcceptOrder(int orderId, AcceptOrderDto acceptOrderDto, int requestingUserId, string role);
    Task<ShowOrderDto> CancelOrder(int orderId, int requestingUserId, string role);
    Task<List<ShowOrderDto>> GetAcceptedOrdersByRestaurant(int restaurantId, int requestingUserId, string role);
    Task<List<ShowOrderDto>> GetCanceledOrdersByRestaurant(int restaurantId, int requestingUserId, string role);
    Task<OrderReviewDto> CreateOrderReviewAsync(int orderId, int customerId, OrderReviewDto orderReviewDto);
    Task<List<ShowOrderDto>> GetOrdersByCustomerId(int customerId, OrderStatus? status);
    Task<ShowOrderDto> AssignCourierToOrderAsync(int orderId, int requestingUserId, string role);
    Task<ShowOrderDto> PickUpOrderAsync(int orderId, int userId);
    Task<ShowOrderDto> OrderDeliveredAsync(int orderId, int userId);

}