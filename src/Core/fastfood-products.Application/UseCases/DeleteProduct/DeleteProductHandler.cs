using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Contracts.Repository;
using MediatR;

namespace fastfood_products.Application.UseCases.DeleteProduct;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Result<DeleteProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<DeleteProductResponse>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        Domain.Entity.ProductEntity? product = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
            return Result<DeleteProductResponse>.Failure("PBE010");

        await _productRepository.DeleteProductAsync(product, cancellationToken);

        return Result<DeleteProductResponse>.Success(new DeleteProductResponse());
    }
}
