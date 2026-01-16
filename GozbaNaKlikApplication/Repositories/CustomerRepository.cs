using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Repositories
{
    public class CustomerRepository
    {
        private AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerProfile> AddNewCustomerAsync(CustomerProfile customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
