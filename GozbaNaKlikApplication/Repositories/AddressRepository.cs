using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Address;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Address> GetCustomerAddressByIdAsync(int id)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Address> AddNewCustomerAddressAsync(Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address;
        }
        public async Task<bool> DeleteAddressAsync(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            _context.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Address>> GetAllCustomerAddressesAsync(int customerId)
        {
            var addresses = await _context.Addresses
                .Where(c => c.CustomerProfileId == customerId)
                .ToListAsync();
            return addresses;
        }

        public async Task<Address> UpdateCustomerAddressAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return address;
        }

    }
}
