
using ECommerce.Domain.Entities.OrderModule;

namespace ECommerce.Inferastructure.Configurations;

public class DeliveryMethodConfiguratin : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(d => d.Price)
            .HasColumnType("decimal(18,4)");
    }
}
