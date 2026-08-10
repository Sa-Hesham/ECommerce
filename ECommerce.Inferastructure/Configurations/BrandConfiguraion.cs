
namespace ECommerce.Inferastructure.Configurations;

public class BrandConfiguraion : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);


        builder.HasIndex(p => p.Name)
            .IsUnique();

        builder.HasKey(p => p.Id);

    }
}
