using fastfood_products.Data.Entity;

namespace fastfood_products.Interface;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken);
    Task<Product?> GetProductById(int productId, CancellationToken cancellationToken);
}
