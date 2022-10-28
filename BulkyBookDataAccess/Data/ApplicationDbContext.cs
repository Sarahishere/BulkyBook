using BulkyBookModels;
using BulkyBookModels.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookDataAccess;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext()
    {
        
    }    
        
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connString = "Data Source=localhost,1433;Initial Catalog=Bulky;User Id=sa;Password=Password0!;TrustServerCertificate=True";
            optionsBuilder
                .UseSqlServer(connString);
        }
        base.OnConfiguring(optionsBuilder);
    }
    
}