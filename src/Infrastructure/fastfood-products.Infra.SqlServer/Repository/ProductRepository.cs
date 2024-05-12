using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Domain.Entity;
using fastfood_products.Domain.Enum;
using fastfood_products.Infra.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace fastfood_products.Infra.SqlServer.Repository;

public class ProductRepository : IProductRepository
{
    protected DbSet<ProductEntity> Data { get; }
    protected ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
        Data = _context.Set<ProductEntity>();
    }

    public async Task<ProductEntity?> GetProductByIdAsync(int productId, CancellationToken cancellationToken)
        => await Data.FindAsync(productId, cancellationToken);

    public async Task CreateProductAsync(ProductEntity product, CancellationToken cancellationToken)
    {
        await Data.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProductAsync(ProductEntity product, CancellationToken cancellationToken)
    {
        Data.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken)
    {
        Data.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryAsync(CategoryType type, CancellationToken cancellationToken)
         => await Data
            .Where(x => x.Type == type)
            .ToListAsync(cancellationToken);
}
