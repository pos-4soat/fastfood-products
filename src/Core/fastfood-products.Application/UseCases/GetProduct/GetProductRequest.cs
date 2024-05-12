using fastfood_products.Application.Shared.BaseResponse;
using MediatR;

namespace fastfood_products.Application.UseCases.GetProduct;

public sealed record GetProductRequest : IRequest<Result<GetProductResponse>>
{
    public int ProductId { get; set; }

    public GetProductRequest(int productId)
    {
        ProductId = productId;
    }
}
