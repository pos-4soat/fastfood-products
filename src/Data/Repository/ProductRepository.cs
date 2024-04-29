using fastfood_products.Data.Context;
using fastfood_products.Data.Entity;
using fastfood_products.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace fastfood_products.Data.Repository;

public class ProductRepository : IProductRepository
{
    protected DbSet<Product> Data { get; }
    protected ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
        Data = _context.Set<Product>();
    }

    public async Task<Product?> GetProductById(int productId, CancellationToken cancellationToken)
        => await Data.FindAsync(productId, cancellationToken);

    public async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken)
        => await Data
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Product>> GetProduct(Expression<Func<Product, bool>> predicated, CancellationToken cancellationToken)
         => await Data
            .Where(predicated)
            .ToListAsync(cancellationToken);
}
