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
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Allergen> Allergens { get; set; }
    public DbSet<Address> Addresses { get; set; }
    
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
        
        //Povezivanje modela Restoran i Jelo:
        modelBuilder.Entity<Restaurant>()
            .HasOne(r => r.Owner)
            .WithMany(o => o.MyRestaurants)
            .HasForeignKey(r => r.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Meal>()
            .HasOne(m => m.Restaurant)
            .WithMany(r => r.Meals)
            .HasForeignKey(r => r.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Allergen>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Allergen>()
            .Property(a => a.Name).IsRequired()
            .HasMaxLength(100);
        
        // Relacija Meal - Allergen
        modelBuilder.Entity<Meal>()
            .HasMany(m => m.MealAllergens)
            .WithMany()
            .UsingEntity(j => j.ToTable("MealAllergens"));
            
        // Relacija Customer - Allergen
        modelBuilder.Entity<CustomerProfile>()
            .HasMany(c => c.CustomerAllergens)
            .WithMany()
            .UsingEntity(j => j.ToTable("CustomerAllergens"));

        modelBuilder.Entity<Address>()
            .HasOne(c => c.CustomerProfile)
            .WithMany(a => a.CustomerAddresses)
            .HasForeignKey(a => a.CustomerProfileId)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<User>()
            .HasData(new User { Id = 1, Username = "Admin1", PasswordHash = "$2a$11$Z/QwBhXbDM1i8YdaUyJCa.ySiEr9Pk7RulGvrN2WdyMauTeEcvdNy", Name = "Aleksandar", Surname = "Popov", Email = "aleksandarpopov@gmail.com", Role = Models.Enums.UserRole.Administrator },
                     new User { Id = 2, Username = "Admin2", PasswordHash = "$2a$11$Z/QwBhXbDM1i8YdaUyJCa.ySiEr9Pk7RulGvrN2WdyMauTeEcvdNy", Name = "Nikola", Surname = "Popovski", Email = "nikolapopovski@gmail.com", Role = Models.Enums.UserRole.Administrator },
                     new User { Id = 3, Username = "Admin3", PasswordHash = "$2a$11$Z/QwBhXbDM1i8YdaUyJCa.ySiEr9Pk7RulGvrN2WdyMauTeEcvdNy", Name = "Petar", Surname = "Nikolic", Email = "petarnikolic@gmail.com", Role = Models.Enums.UserRole.Administrator });

        modelBuilder.Entity<AdministratorProfile>()
            .HasData(new AdministratorProfile { UserId = 1 },
                     new AdministratorProfile { UserId = 2 },
                     new AdministratorProfile { UserId = 3 });
        
        modelBuilder.Entity<Allergen>().HasData(
            new Allergen { Id = 1, Name = "Gluten" },
            new Allergen { Id = 2, Name = "Lactose" },
            new Allergen { Id = 3, Name = "Peanuts" },
            new Allergen { Id = 4, Name = "Soy" },
            new Allergen { Id = 5, Name = "Eggs" },
            new Allergen { Id = 6, Name = "Fish"}
        );

    }
}