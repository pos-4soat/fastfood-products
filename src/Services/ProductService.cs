using fastfood_products.Constants;
using fastfood_products.Data.Entity;
using fastfood_products.Interface;
using fastfood_products.Models.Base;
using fastfood_products.Models.Request;
using fastfood_products.Models.Response;

namespace fastfood_products.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CreateProductResponse>> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        _ = await _repository.GetProducts(cancellationToken);
        return Result<CreateProductResponse>.Success(new CreateProductResponse(), StatusEnum.CREATED);
    }

    public async Task<Result<GetProductResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Product> result = await _repository.GetProducts(cancellationToken);


        List<ProductBaseResponse> products = [];
        foreach (Product productEntity in result)
        {
            ProductBaseResponse product = new(productEntity);
            products.Add(product);
        };

        return Result<GetProductResponse>.Success(new GetProductResponse(products));
    }
}
