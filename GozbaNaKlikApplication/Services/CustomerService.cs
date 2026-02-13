using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Services.Interfaces;

namespace GozbaNaKlikApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerProfile> AddCustomerAsync(CustomerProfile customerProfile)
        {
            CustomerProfile customer = await _customerRepository.AddNewCustomerAsync(customerProfile);
            return customer;
        }
    }
}
