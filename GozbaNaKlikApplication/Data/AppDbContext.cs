using GozbaNaKlikApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    //TODO: Definisati odgovarajuce DbSetove za postojece modele.
    public DbSet<User> Users { get; set; }
    public DbSet<AdministratorProfile> Administrators { get; set; }
    public DbSet<OwnerProfile> Owners { get; set; }
    public DbSet<CustomerProfile> Customers { get; set; }
    public DbSet<CourierProfile> Couriers { get; set; }

    //TODO: Definisati seed za dodavanje 3 administratora
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.CustomerProfile)
            .WithOne(c => c.User)
            .HasForeignKey<CustomerProfile>(c => c.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.AdministratorProfile)
            .WithOne(a => a.User)
            .HasForeignKey<AdministratorProfile>(a => a.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.OwnerProfile)
            .WithOne(o => o.User)
            .HasForeignKey<OwnerProfile>(o => o.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.CourierProfile)
            .WithOne(c => c.User)
            .HasForeignKey<CourierProfile>(c => c.UserId);

        //Postavljamo da nam strani kljuc ujedno bude i primarni
        modelBuilder.Entity<CustomerProfile>()
            .HasKey(c => c.UserId);
        modelBuilder.Entity<AdministratorProfile>()
            .HasKey(a => a.UserId);
        modelBuilder.Entity<OwnerProfile>()
            .HasKey(o => o.UserId);
        modelBuilder.Entity<CourierProfile>()
            .HasKey(c => c.UserId);

        modelBuilder.Entity<User>()
            .HasData(new User { Id = 1, Username = "Admin1", PasswordHash = "admin1", Name = "Aleksandar", Surname = "Popov", Email = "aleksandarpopov@gmail.com", Role = Models.Enums.UserRole.Administrator },
                     new User { Id = 2, Username = "Admin2", PasswordHash = "admin2", Name = "Nikola", Surname = "Popovski", Email = "nikolapopovski@gmail.com", Role = Models.Enums.UserRole.Administrator },
                     new User { Id = 3, Username = "Admin3", PasswordHash = "admin3", Name = "Petar", Surname = "Nikolic", Email = "petarnikolic@gmail.com", Role = Models.Enums.UserRole.Administrator });

        modelBuilder.Entity<AdministratorProfile>()
            .HasData(new AdministratorProfile { UserId = 1 },
                     new AdministratorProfile { UserId = 2 },
                     new AdministratorProfile { UserId = 3 });
    }
}