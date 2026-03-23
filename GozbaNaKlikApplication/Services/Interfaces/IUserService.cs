using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces;

public interface IUserService
{
    Task<User> Login(string username, string password);
    string GenerateJwtToken(User user);
    Task<User> AddUserAsync(User user);
    Task<PaginatedList<UserPreviewDto>> GetAllUsersPagedAsync(int page, int pageSize);
    Task<User?> GetByUsername(string username);
}