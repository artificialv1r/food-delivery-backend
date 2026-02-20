using AutoMapper;
using GozbaNaKlikApplication.DTOs.Address;
using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;
        public AddressService(IAddressRepository addressRepository, IUserRepository userRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<CreateCustomerAddressDto> AddNewCustomerAddressAsync(CreateCustomerAddressDto addressDto, int customerId)
        {
            var user = await _userRepository.GetByIdAsync(customerId);
            var address = _mapper.Map<Address>(addressDto);
            var createdAddress = await _addressRepository.AddNewCustomerAddressAsync(address);
            return _mapper.Map<CreateCustomerAddressDto>(createdAddress);
        }
        public async Task<bool> DeleteAddressAsync(int id, int customerId)
        {
            var address = await _addressRepository.GetCustomerAddressByIdAsync(id);
            if (address == null)
            {
                throw new NotFoundException(id);
            }
            if(address.CustomerProfileId != customerId)
            {
                throw new ForbiddenException("You do not have permission to delete this address.");
            }
            return await _addressRepository.DeleteAddressAsync(id);
        }

        public async Task<List<ShowAddressDto>> GetAllCustomerAddressesAsync(int customerId)
        {
            var user = await _userRepository.GetByIdAsync(customerId);
            var addresses = await _addressRepository.GetAllCustomerAddressesAsync(customerId);
            var addressesDto = _mapper.Map<List<ShowAddressDto>>(addresses);
            return addressesDto;
        }
        public async Task<UpdateAddressDto> UpdateCustomerAddressAsync(int customerId, int addressId, UpdateAddressDto addressDto)
        {
            var user = await _userRepository.GetByIdAsync(customerId);
            var existingAddress = await _addressRepository.GetCustomerAddressByIdAsync(addressId);
            if (existingAddress == null)
            {
                throw new NotFoundException(addressId);
            }

            _mapper.Map(addressDto, existingAddress);

            var updatedAddress = await _addressRepository.UpdateCustomerAddressAsync(existingAddress);

            return _mapper.Map<UpdateAddressDto>(updatedAddress);
        }
    }
}
