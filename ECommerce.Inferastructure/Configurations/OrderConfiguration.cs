using ECommerce.Domain.Entities.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Inferastructure.Configurations;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(o => o.Address, sh => sh.WithOwner());
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.HasOne(o=>o.DeliveryMethod)
         .WithMany()
        .HasForeignKey(o => o.DeliveryMethodId).OnDelete(DeleteBehavior.SetNull);

        builder.Property(o => o.OrderPaymentStatus)
            .HasConversion(ps => ps.ToString(), ps => Enum.Parse<OrderPaymentStatus>(ps));


        builder.Property(o => o.SubTotal)
            .HasColumnType("decimal(18,4)");
            
    }
}
