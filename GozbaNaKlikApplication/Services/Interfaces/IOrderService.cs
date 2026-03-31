using GozbaNaKlikApplication.DTOs.Orders;

namespace GozbaNaKlikApplication.Services.Interfaces;

public interface IOrderService
{
    Task<ShowOrderDto> CreateOrder(CreateOrderDto orderDto, int customerId);
    Task<OrderReviewDto> CreateOrderReviewAsync(int orderId, int customerId, OrderReviewDto orderReviewDto);

}