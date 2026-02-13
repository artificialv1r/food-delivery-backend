using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories;

public class OwnerRepository : IOwnerRepository
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

    public async Task<OwnerProfile> GetByUserId(int id)
    {
        return await _context.Owners.FirstOrDefaultAsync(o => o.UserId == id);
    }
}