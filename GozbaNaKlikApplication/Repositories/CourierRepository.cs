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
}