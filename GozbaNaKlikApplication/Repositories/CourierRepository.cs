using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
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

}