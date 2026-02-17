using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Repositories;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GozbaNaKlikApplication.Services;

public class AdministratorService : IAdministratorService
{
    private readonly IUserRepository _userRepository;
    private readonly IOwnerService _ownerService;
    private readonly ICourierService _courierService;


    public AdministratorService(IUserRepository userRepository, IOwnerService ownerService, ICourierService courierService)
    {
        _userRepository = userRepository;
        _ownerService = ownerService;
        _courierService = courierService;
    }

    public async Task<User> RegisterNewUser(User user)
    {
        if (user.Role != UserRole.Owner && user.Role != UserRole.Courier)
        {
            throw new BadRequestException("Invalid role for admin registration");
        }

        var existingUser = await _userRepository.GetByUsername(user.Username);

        if (existingUser != null)
        {
            throw new BadRequestException("This user already exists");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        User newUser = await _userRepository.AddNewUserAsync(user);

        switch (user.Role)
        {
            case UserRole.Owner:
                await _ownerService.AddOwnerAsync(new OwnerProfile { UserId = newUser.Id });
                break;

            case UserRole.Courier:
                await _courierService.AddCourierAsync(new CourierProfile { UserId = newUser.Id });
                break;
        }

        return newUser;
    }
}