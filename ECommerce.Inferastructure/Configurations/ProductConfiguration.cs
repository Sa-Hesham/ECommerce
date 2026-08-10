using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ECommerce.Inferastructure.Configurations;

    public  class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
              .IsRequired()
              .HasMaxLength(50);


        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(700);



        builder.HasKey(p => p.Id);


        builder.Property(p => p.PictureUrl)
            .IsRequired(false)
            .HasMaxLength(500);



        builder.Property(p => p.price)
            .HasPrecision(18, 2)
            .IsRequired();



        builder.HasOne(p => p.Brand)
            .WithMany(B => B.products)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);



        builder.HasOne(p => p.ProductType)
            .WithMany(pt=>pt.products)
            .HasForeignKey(p => p.ProductTypeId)   
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.price);
    }
}
