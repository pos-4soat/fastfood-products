using fastfood_products.Domain.Entity;
using fastfood_products.Domain.Enum;

namespace fastfood_products.Domain.Contracts.Repository;

public interface IProductRepository
{
    Task CreateProductAsync(ProductEntity product, CancellationToken cancellationToken);
    Task DeleteProductAsync(ProductEntity product, CancellationToken cancellationToken);
    Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken);
    Task<ProductEntity?> GetProductByIdAsync(int productId, CancellationToken cancellationToken);
    Task<IEnumerable<ProductEntity>> GetProductsByCategoryAsync(CategoryType type, CancellationToken cancellationToken);
}
