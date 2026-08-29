
using ECommerce.Domain.Entities.OrderModule;

namespace ECommerce.Inferastructure.Data;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext):base(dbContext)
    {
        
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductBrand> Brands { get; set; } 

    public DbSet<ProductType> ProductTypes { get; set; }
    
    public DbSet<Order> Orders { get; set; } 
    public DbSet<OrderItem> OrderItems { get; set; } 
    public DbSet<DeliveryMethod> DeliveryMethods { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
