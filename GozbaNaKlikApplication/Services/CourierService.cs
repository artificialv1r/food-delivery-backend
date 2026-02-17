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
        var existing = await _userRepository.GetByUsername(courier.User.Username);
        if (existing != null)
        {
            throw new BadRequestException("This user already exists");
        }
        return await _courierRepository.AddNewCourierAsync(courier);
    }
}