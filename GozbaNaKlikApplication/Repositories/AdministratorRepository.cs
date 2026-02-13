using GozbaNaKlikApplication.Data;
using GozbaNaKlikApplication.DTOs.Auth;
using GozbaNaKlikApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Repositories
{
    public class AdministratorRepository
    {
        private AppDbContext _context;

        public AdministratorRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
