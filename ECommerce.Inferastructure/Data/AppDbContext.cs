
namespace ECommerce.Inferastructure.Data;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext):base(dbContext)
    {
        
    }
}
