using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Repositories;

public class UserRepository
{
    private AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    //TODO: Dodati metode za rad sa korisnicima (npr. GetAll, GetOne, Add, Update, Delete)

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    
}