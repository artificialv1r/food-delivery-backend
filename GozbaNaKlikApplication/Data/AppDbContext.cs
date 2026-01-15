using GozbaNaKlikApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options):base(options){}
    
    //TODO: Definisati odgovarajuce DbSetove za postojece modele.
    public DbSet<User> Users { get; set; }
    public DbSet<AdministratorProfile> Administrators { get; set; }
    public DbSet<OwnerProfile> Owners { get; set; }
    public DbSet<CustomerProfile> Customers { get; set; }
    public DbSet<CourierProfile> Courses { get; set; }

    //TODO: Definisati seed za dodavanje 3 administratora
    
}