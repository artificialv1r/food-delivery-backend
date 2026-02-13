using GozbaNaKlikApplication.DTOs.Auth;

namespace GozbaNaKlikApplication.Models.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsername(string username);
    Task<User> AddNewUserAsync(User user);
    Task<List<UserPreviewDto>> GetPagedAsync(int page, int pageSize, string orderDirection);
    Task<int> CountAllUsersAsync();
}