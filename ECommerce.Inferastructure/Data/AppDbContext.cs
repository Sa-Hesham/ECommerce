
namespace ECommerce.Inferastructure.Data;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext):base(dbContext)
    {
        
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductBrand> Brands { get; set; } 

    public DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
