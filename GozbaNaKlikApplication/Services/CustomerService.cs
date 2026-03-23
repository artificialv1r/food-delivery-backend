using GozbaNaKlikApplication.Exceptions;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Models.Interfaces;
using GozbaNaKlikApplication.Repositories;
using GozbaNaKlikApplication.Services.Interfaces;
using System.Diagnostics.Metrics;

namespace GozbaNaKlikApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<CustomerProfile> AddCustomerAsync(CustomerProfile customer)
        {
            return await _customerRepository.AddNewCustomerAsync(customer);
        }
    }
}
