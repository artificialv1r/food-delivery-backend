using GozbaNaKlikApplication.DTOs.Auth;

namespace GozbaNaKlikApplication.Models.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsername(string username);
    Task<User> AddNewUserAsync(User user);
    Task<PaginatedList<User>> GetPagedAsync(int page, int pageSize);
}