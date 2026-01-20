using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Repositories;

namespace GozbaNaKlikApplication.Services;

public class CourierService
{
    private readonly CourierRepository _courierRepository;

    public CourierService(AppDbContext context)
    {
        _courierRepository = new CourierRepository(context);
    }

    public async Task<CourierProfile> AddCourierAsync(CourierProfile courier)
    {
        return await _courierRepository.AddNewCourierAsync(courier);
    }
}