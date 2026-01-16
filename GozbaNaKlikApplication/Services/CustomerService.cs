using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Repositories;

namespace GozbaNaKlikApplication.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(AppDbContext context)
        {
            _customerRepository = new CustomerRepository(context);
        }

        public async Task<CustomerProfile> AddCustomerAsync(CustomerProfile customerProfile)
        {
            CustomerProfile customer = await _customerRepository.AddNewCustomerAsync(customerProfile);
            return customer;
        }
    }
}
