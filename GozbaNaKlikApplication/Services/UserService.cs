using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.Models;
using GozbaNaKlikApplication.Repositories;

namespace GozbaNaKlikApplication.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(AppDbContext context)
    {
        _userRepository = new UserRepository(context);
    }
    
    //TODO: Dodati servise za korisnike (npr. GetAll, GetOne, Create, Update, Delete)

    public async Task<User> AddNewUserAsync(User user)
    {
        User newUser = await _userRepository.AddAsync(user);
        if(newUser == null)
        {
            throw new Exception("Something went wrong. You sholud try again.");
        }
        return newUser;
    }
}