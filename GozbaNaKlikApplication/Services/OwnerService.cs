using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Repositories;
using GozbaNaKlikApplication.Services.Interfaces;
using System.Diagnostics.Metrics;

namespace GozbaNaKlikApplication.Services;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly IUserRepository _userRepository;

    public OwnerService(IOwnerRepository ownerRepository, IUserRepository userRepository)
    {
        _ownerRepository = ownerRepository;
        _userRepository = userRepository;
    }

    public async Task<OwnerProfile> AddOwnerAsync(OwnerProfile owner)
    {
        var existing = await _userRepository.GetByUsername(owner.User.Username);
        if (existing != null)
        {
            throw new BadRequestException("This user already exists");
        }
        return await _ownerRepository.AddNewOwnerAsync(owner);
    }
}