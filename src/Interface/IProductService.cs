using fastfood_products.Models.Base;
using fastfood_products.Models.Request;
using fastfood_products.Models.Response;

namespace fastfood_products.Interface;

public interface IProductService
{
    Task<Result<GetProductResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<CreateProductResponse>> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);
}
