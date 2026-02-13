namespace GozbaNaKlikApplication.Models.Interfaces;

public interface ICustomerRepository
{
    Task<CustomerProfile> AddNewCustomerAsync(CustomerProfile customer);
}