using GozbaNaKlikApplication.DTOs.Address;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IAddressService
    {
        Task<CreateAddressDto> AddNewAddressAsync(CreateAddressDto addressDto, int customerId);
        Task<bool> DeleteAddressAsync(int id);
        Task<List<ShowAddressDto>> GetAllAddressesAsync(int custmerId);
        Task<UpdateAddressDto> UpdateAddressAsync(int customerId, int addressId,UpdateAddressDto address);
    }
}