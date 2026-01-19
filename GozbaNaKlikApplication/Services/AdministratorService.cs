using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GozbaNaKlikApplication.Services;

public class AdministratorService
{
    private readonly UserRepository _userRepository;
    private readonly OwnerService _ownerService;
    private readonly CourierService _courierService;
    
    public AdministratorService(AppDbContext context){
        _userRepository = new UserRepository(context);
        _ownerService = new OwnerService(context);
        _courierService = new CourierService(context);
    }

    public async Task<User> RegisterNewUser(User user)
    {
        if (user.Role != UserRole.Owner && user.Role != UserRole.Courier)
        {
            throw new ArgumentException("Invalid role for admin registration");
        }
        
        var existingUser = await _userRepository.GetByUsername(user.Username);

        if (existingUser != null)
        {
            throw new InvalidOperationException("This user already exists");
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