using GozbaNaKlikApplication.Data;

namespace GozbaNaKlikApplication.Repositories;

public class UserRepository
{
    private AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    //TODO: Dodati metode za rad sa korisnicima (npr. GetAll, GetOne, Add, Update, Delete)
    
}