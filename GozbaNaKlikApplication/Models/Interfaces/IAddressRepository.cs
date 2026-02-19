using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> AddNewAddressAsync(Address address);
        Task<bool> DeleteAddressAsync(int id);
        Task<Address> GetByIdAsync(int id);
        Task<List<Address>> GetAllAddressesAsync(int customerId);
        Task<Address> UpdateAddressAsync(Address address);
    }
}