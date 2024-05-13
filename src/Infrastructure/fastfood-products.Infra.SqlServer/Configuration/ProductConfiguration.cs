using fastfood_products.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace fastfood_products.Infra.SqlServer.Configuration;

[ExcludeFromCodeCoverage]
public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(c => c.Id).HasName("ProductId");
        builder.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(255);
        builder.Property(c => c.Type).HasColumnName("Type");
        builder.Property(c => c.Price).HasColumnName("Price").HasPrecision(18, 2);
        builder.Property(c => c.Description).HasColumnName("Description");
        builder.Property(c => c.ProductImageUrl).HasColumnName("ProductImageUrl");
    }
}
