using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IOwnerRepository
    {
        Task<OwnerProfile> GetByUserId(int id);
        Task<OwnerProfile> AddNewOwnerAsync(OwnerProfile owner);
    }
}