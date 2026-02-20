using GozbaNaKlikApplication.DTOs.Address;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IAddressService
    {
        Task<CreateCustomerAddressDto> AddNewCustomerAddressAsync(CreateCustomerAddressDto addressDto, int customerId);
        Task<bool> DeleteAddressAsync(int id, int customerId);
        Task<List<ShowAddressDto>> GetAllCustomerAddressesAsync(int custmerId);
        Task<UpdateAddressDto> UpdateCustomerAddressAsync(int customerId, int addressId,UpdateAddressDto address);
    }
}