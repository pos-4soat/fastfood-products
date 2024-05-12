using fastfood_products.Application.Shared.BaseResponse;
using MediatR;

namespace fastfood_products.Application.UseCases.DeleteProduct;

public sealed record DeleteProductRequest : IRequest<Result<DeleteProductResponse>>
{
    public int ProductId { get; set; }

    public DeleteProductRequest(int productId)
    {
        ProductId = productId;
    }
}
