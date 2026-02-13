using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces;

public interface IUserService
{
    Task<User> Login(string username, string password);
    string GenerateJwtToken(User user);
    Task<User> AddUserAsync(User user);
    Task<List<UserPreviewDto>> GetAllUsers(int page, int pageSize, string orderDirection);
    Task<int> CountAllUsers();
}