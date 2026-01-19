using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Repositories;

public class CourierRepository
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