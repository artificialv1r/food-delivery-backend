using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories;

public class UserRepository  : IUserRepository
{
    private AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> AddNewUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
    public async Task<List<UserPreviewDto>> GetPagedAsync(int page, int pageSize, string orderDirection)
    {
        IQueryable<User> query = _context.Users;

        query = orderDirection == "desc"
            ? query.OrderByDescending(u => u.Username)
            : query.OrderBy(u => u.Username);

        query = query.Skip(pageSize * (page - 1))
            .Take(pageSize);

        var result = query.Select(u => new UserPreviewDto
        {
            Username = u.Username,
            Name = u.Name,
            Surname = u.Surname,
            Email = u.Email,
            Role = u.Role.ToString()
        });
        return await result.ToListAsync();
    }
    public async Task<int> CountAllUsersAsync()
    {
        return await _context.Users.CountAsync();
    }
}