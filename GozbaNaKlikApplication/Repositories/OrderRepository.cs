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

    public async Task<Order?> GetActiveCourierOrder(int courierId)
    {
        return await _context.Orders
            .Include(o => o.MealsOrdered)
                .ThenInclude(OrderMeal => OrderMeal.Meal)
            .Include(o => o.CustomerProfile)
                .ThenInclude(cp => cp.User)
            .FirstOrDefaultAsync(o =>
                          o.CourierId == courierId &&        
                          (o.OrderStatus == OrderStatus.PickupInProgress ||
                          o.OrderStatus == OrderStatus.DeliveryInProgress));
    }

}