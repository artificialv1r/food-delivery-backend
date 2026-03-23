using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;

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
}