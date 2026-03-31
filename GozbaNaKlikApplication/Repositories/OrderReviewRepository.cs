using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories
{
    public class OrderReviewRepository : IOrderReviewRepository
    {
        private AppDbContext _context;

        public OrderReviewRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<OrderReview> CreateOrderReviewAsync(OrderReview orderReview)
        {
            _context.OrderReviews.Add(orderReview);
            await _context.SaveChangesAsync();
            return orderReview;
        }

        public async Task<OrderReview?> GetReviewByOrderId(int orderId)
        {
            return await _context.OrderReviews
                .FirstOrDefaultAsync(r => r.OrderId == orderId);
        }

    }
}
