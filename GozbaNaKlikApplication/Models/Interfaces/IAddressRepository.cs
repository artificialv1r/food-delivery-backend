using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Models.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> AddNewCustomerAddressAsync(Address address);
        Task<bool> DeleteAddressAsync(int id);
        Task<Address> GetCustomerAddressByIdAsync(int id);
        Task<List<Address>> GetAllCustomerAddressesAsync(int customerId);
        Task<Address> UpdateCustomerAddressAsync(Address address);
    }
}