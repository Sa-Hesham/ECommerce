using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Inferastructure.Configurations;

internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.Property(p => p.Name)
           .IsRequired()
           .HasMaxLength(50);


        builder.HasIndex(p => p.Name)
            .IsUnique();





        builder.HasKey(p => p.Id);

    }

    
}

