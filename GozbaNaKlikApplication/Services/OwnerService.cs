using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Repositories;

namespace GozbaNaKlikApplication.Services;

public class OwnerService
{
    private readonly OwnerRepository _ownerRepository;

    public OwnerService(AppDbContext context)
    {
        _ownerRepository = new OwnerRepository(context);
    }

    public async Task<OwnerProfile> AddOwnerAsync(OwnerProfile owner)
    {
        return await _ownerRepository.AddNewOwnerAsync(owner);
    }
}