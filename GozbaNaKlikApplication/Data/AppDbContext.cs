using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlikApplication.Data;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options):base(options){}
    
    //TODO: Definisati odgovarajuce DbSetove za postojece modele.
    
    //TODO: Definisati seed za dodavanje 3 administratora
    
}