using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Repositories;

public class OwnerRepository
{
    private readonly AppDbContext _context;
    
    public OwnerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OwnerProfile> AddNewOwnerAsync(OwnerProfile owner)
    {
        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();
        return owner;
    }
}