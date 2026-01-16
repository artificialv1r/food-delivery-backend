using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace GozbaNaKlikApplication.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(AppDbContext context)
    {
        _userRepository = new UserRepository(context);
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
    
    //TODO: Dodati servise za korisnike (npr. GetAll, GetOne, Create, Update, Delete)

    public async Task<User> AddNewUserAsync(User user)
    {
        User newUser = await _userRepository.AddAsync(user);
        if(newUser == null)
        {
            throw new Exception("Something went wrong. You sholud try again.");
        }
        return newUser;
    }
}