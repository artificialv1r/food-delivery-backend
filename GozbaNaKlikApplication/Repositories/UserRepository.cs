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

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
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
    public async Task<PaginatedList<User>> GetAllUsersPagedAsync(int page, int pageSize)
    {
        int pageIndex = page - 1;
        var query = _context.Users;

        var users = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var count = await _context.Users.CountAsync();
        PaginatedList<User> result = new PaginatedList<User>(users, count, pageIndex, pageSize);
        return result;
    }
   
}