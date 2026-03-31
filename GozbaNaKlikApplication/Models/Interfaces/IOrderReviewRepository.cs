using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IOrderReviewRepository
    {
        Task<OrderReview> CreateOrderReviewAsync(OrderReview orderReview);
        Task<OrderReview?> GetReviewByOrderId(int orderId);
    }
}