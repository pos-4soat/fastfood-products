using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Enum;
using MediatR;

namespace fastfood_products.Application.UseCases.CreateProduct;

public sealed record CreateProductRequest
    (
    string Name,
    CategoryType Type,
    decimal Price,
    string Description,
    string ProductImageUrl) : IRequest<Result<CreateProductResponse>>;
