using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Repositories;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Services;

public class CourierService : ICourierService
{
    private readonly ICourierRepository _courierRepository;
    private readonly IUserRepository _userRepository;

    public CourierService(ICourierRepository courierRepository, IUserRepository userRepository)
    {
        _courierRepository = courierRepository;
        _userRepository = userRepository;
    }

    public async Task<CourierProfile> AddCourierAsync(CourierProfile courier)
    {
        return await _courierRepository.AddNewCourierAsync(courier);
    }
}