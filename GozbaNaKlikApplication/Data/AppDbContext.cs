using GozbaNaKlikApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options):base(options){}
    
    //TODO: Definisati odgovarajuce DbSetove za postojece modele.
    public DbSet<User> Users { get; set; }
    
    //TODO: Definisati seed za dodavanje 3 administratora
    
}