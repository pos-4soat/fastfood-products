using fastfood_products.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fastfood_products.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        Console.WriteLine(nameof(Configure));
        _ = builder.ToTable("Product");
        _ = builder.HasKey(c => c.Id).HasName("ProductId");
        _ = builder.Property(c => c.Id).HasColumnName("Id").ValueGeneratedNever().UseIdentityColumn();
        _ = builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(255);
        _ = builder.Property(c => c.Type).HasColumnName("Type");
        _ = builder.Property(c => c.Price).HasColumnName("Price").HasPrecision(18, 2);
        _ = builder.Property(c => c.Description).HasColumnName("Description");
        _ = builder.Property(c => c.ProductImageUrl).HasColumnName("ProductImageUrl");
        Console.WriteLine("Finish");
    }
}
