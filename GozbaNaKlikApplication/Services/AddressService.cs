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
        public async Task<CreateAddressDto> AddNewAddressAsync(CreateAddressDto addressDto, int customerId)
        {
            var user = await _userRepository.GetByIdAsync(customerId);
            if (user == null)
            {
                throw new NotFoundException(customerId);
            }
            if (user.Role != Models.Enums.UserRole.Customer)
            {
                throw new ForbiddenException($"User with id {customerId} is not a customer.");
            }
            var address = _mapper.Map<Address>(addressDto);
            address.CustomerProfileId = customerId;
            var createdAddress = await _addressRepository.AddNewAddressAsync(address);
            return _mapper.Map<CreateAddressDto>(createdAddress);
        }
        public async Task<bool> DeleteAddressAsync(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null)
            {
                throw new NotFoundException(id);
            }
            return await _addressRepository.DeleteAddressAsync(id);
        }

        public async Task<List<ShowAddressDto>> GetAllAddressesAsync(int customerId)
        {
            var user = await _userRepository.GetByIdAsync(customerId);
            if (user == null)
            {
                throw new NotFoundException(customerId);
            }
            var addresses = await _addressRepository.GetAllAddressesAsync(customerId);
            var addressesDto = _mapper.Map<List<ShowAddressDto>>(addresses);
            return addressesDto;
        }
        public async Task<UpdateAddressDto> UpdateAddressAsync(int customerId, int addressId, UpdateAddressDto addressDto)
        {
            var user = await _userRepository.GetByIdAsync(customerId);
            if (user == null)
            {
                throw new NotFoundException(customerId);
            }
            var existingAddress = await _addressRepository.GetByIdAsync(addressId);
            if (existingAddress == null)
                throw new NotFoundException(addressId);

            if (existingAddress.CustomerProfileId != customerId)
                throw new ForbiddenException("You cannot update this address.");

            _mapper.Map(addressDto, existingAddress);

            var updatedAddress = await _addressRepository.UpdateAddressAsync(existingAddress);

            return _mapper.Map<UpdateAddressDto>(updatedAddress);
        }
    }
}
