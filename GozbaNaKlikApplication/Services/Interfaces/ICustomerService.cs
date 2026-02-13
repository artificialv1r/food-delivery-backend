using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces;

public interface ICustomerService
{
    Task<CustomerProfile> AddCustomerAsync(CustomerProfile customerProfile);
}