using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Enum;
using MediatR;

namespace fastfood_products.Application.UseCases.GetProductByCategory;

public sealed record GetProductByCategoryRequest : IRequest<Result<GetProductByCategoryResponse>>
{
    public CategoryType Type { get; set; }

    public GetProductByCategoryRequest(CategoryType type)
    {
        Type = type;
    }
}
