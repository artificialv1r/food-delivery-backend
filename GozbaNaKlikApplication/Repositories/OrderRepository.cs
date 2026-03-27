using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Orders;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
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

    public async Task<Order> UpdateOrder(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.MealsOrdered)
            .ThenInclude(om => om.Meal)
            .FirstAsync(o => o.Id == order.Id);
    }
    
    public async Task<List<Order>> GetPendingOrdersByRestaurant(int restaurantId)
    {
        return await _context.Orders
            .Where(o => o.RestaurantId == restaurantId && o.OrderStatus == OrderStatus.OnHold)
            .Include(o => o.Restaurant)
            .Include(o => o.MealsOrdered)
            .ThenInclude(om => om.Meal)
            .OrderBy(o => o.CreatedAt)
            .ToListAsync();
    }
 
    public async Task<Order?> GetOrderById(int orderId)
    {
        return await _context.Orders
            .Include(o => o.Restaurant)
            .Include(o => o.MealsOrdered)
            .ThenInclude(om => om.Meal)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

}