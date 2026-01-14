using GozbaNaKlikApplication.Data;
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
}