using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Enums;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace GozbaNaKlikApplication.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICustomerService _customerService;

    public UserService(IUserRepository userRepository, ICustomerService customerService)
    {
        _userRepository = userRepository;
        _customerService = customerService;
    }
    
    public async Task<User> Login(string username, string password)
    {
        User user = await _userRepository.GetByUsername(username);

        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }

        bool validPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        if (!validPassword)
        {
            throw new Exception("Invalid username or password");
        }

        return user;
    }

    public async Task<User> AddUserAsync(User user)
    {
        if (user.Role != UserRole.Customer)
        {
            throw new Exception("Invalid role for user registration. ");
        }

        var existingUser = await _userRepository.GetByUsername(user.Username);

        if (existingUser != null)
        {
            throw new Exception("This username already exisist.");
        }

        var password = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        user.PasswordHash = password;

        User newUser = await _userRepository.AddNewUserAsync(user);

        CustomerProfile customer = new CustomerProfile { UserId = newUser.Id };
        await _customerService.AddCustomerAsync(customer);

        return newUser;
    }
    
    public async Task<List<UserPreviewDto>> GetAllUsers(int page, int pageSize, string orderDirection)
    {
        return await _userRepository.GetPagedAsync(page, pageSize, orderDirection);
    }
    public async Task<int> CountAllUsers()
    {
        return await _userRepository.CountAllUsersAsync();
    }
    
    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("THIS_IS_A_VERY_SECRET_KEY_1234567890_ABCD")
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "gozbanaklik",
            audience: "gozbanaklik",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}