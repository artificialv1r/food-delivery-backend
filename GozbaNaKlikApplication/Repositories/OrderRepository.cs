using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories;

public class OrderRepository : IOrderRepository
{
    private AppDbContext _context;
    
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.CourierProfile)
            .Include(o => o.CustomerProfile)
            .Include(o => o.OrderStatus)
            .Include(o => o.MealsOrdered)
                .ThenInclude(om => om.Meal)
            .Include(o => o.OrderReview)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }
    public async Task<Order> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.MealsOrdered)
            .ThenInclude(om => om.Meal)
            .FirstAsync(o => o.Id == order.Id);
    }

    public async Task<OrderReview> CreateOrderReviewAsync(OrderReview orderReview)
    {
        _context.OrderReviews.Add(orderReview);
        await _context.SaveChangesAsync();
        return orderReview;
    }

}