using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Courier;
using GozbaNaKlikApplication.DTOs.Restaurant;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories;

public class CourierRepository : ICourierRepository
{
    private readonly AppDbContext _context;

    public CourierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CourierProfile> AddNewCourierAsync(CourierProfile courier)
    {
        _context.Couriers.Add(courier);
        await _context.SaveChangesAsync();
        return courier;
    }

    public async Task<CourierProfile> UpdateCourier(CourierProfile courier)
    {
        _context.Couriers.Update(courier);
        await _context.SaveChangesAsync();
        return courier;
    }
    public async Task<CourierProfile> GetAvailableCourierAsync()
    {
        var courier = await _context.Couriers
           .Where(c => c.Status == true && c.IsAvailable == true)
           .FirstOrDefaultAsync();
        return courier;
    }
    public async Task<CourierProfile> GetCourierByIdAsync(int id)
    {
        return await _context.Couriers
                .FirstOrDefaultAsync(c => c.UserId == id);
    }
    public async Task<CourierWorkingHours> AddCourireWorkingHoursAsync(CourierWorkingHours courierWorkingHours)
    {
        _context.CourierWorkingHours.Add(courierWorkingHours);
        await _context.SaveChangesAsync();
        return courierWorkingHours;
    }

    public async Task<CourierWorkingHours> GetCourierWorkingHoursByIdAsync(int id, int workingHoursId)
    {
        return await _context.CourierWorkingHours
            .FirstOrDefaultAsync(c => c.CourierId == id && c.Id == workingHoursId);
    }

    public async Task<CourierWorkingHours> UpdateCourierWorkingHoursAsync(CourierWorkingHours courierWorkingHours)
    {
        _context.CourierWorkingHours.Update(courierWorkingHours);
        await _context.SaveChangesAsync();
        return courierWorkingHours;
    }

    public async Task<PaginatedList<Order>> GetFilteredAndSortedDeliveredOrdersAsync(int courierId, OrderSearchQuery orderSearchQuery, int page = 1, int pageSize = 5)
    {
        IQueryable<Order> orders = _context.Orders
            .Include(o => o.CourierProfile)
            .Include(o => o.Restaurant)
            .Include(o => o.MealsOrdered)
                .ThenInclude(o => o.Meal);

        orders = FilterOrders(orders, orderSearchQuery);

        int pageIndex = page - 1;
        var count = await orders.CountAsync();
        var items = await orders
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Where(o => o.CourierId == courierId && o.OrderStatus == OrderStatus.Delivered)
            .OrderByDescending(o => o.DeliveredAt)
            .ToListAsync();

        PaginatedList<Order> result = new PaginatedList<Order>(items, count, pageIndex, pageSize);
        return result;
    }

    private static IQueryable<Order> FilterOrders(IQueryable<Order> orders, OrderSearchQuery filter)
    {
        if (filter.StartDate.HasValue)
        {
            var startDate = DateTime.SpecifyKind(filter.StartDate.Value.Date, DateTimeKind.Utc);
            orders = orders.Where(o => o.DeliveredAt >= startDate);
        }

        if (filter.EndDate.HasValue)
        {
            var endDate = DateTime.SpecifyKind(filter.EndDate.Value.Date.AddDays(1), DateTimeKind.Utc);
            orders = orders.Where(o => o.DeliveredAt < endDate);
        }

        return orders;
    }
}