using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IOwnerService
    {
        Task<OwnerProfile> AddOwnerAsync(OwnerProfile owner);
    }
}