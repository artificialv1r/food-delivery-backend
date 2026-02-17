using GozbaNaKlikApplication.Models;

namespace GozbaNaKlikApplication.Services.Interfaces
{
    public interface IAdministratorService
    {
        Task<User> RegisterNewUser(User user);
    }
}