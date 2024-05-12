using fastfood_products.Application.UseCases.GetProductByCategory;
using FluentValidation;

namespace fastfood_products.Application.UseCases.GetProduct;

public class GetProductValidator : AbstractValidator<GetProductByCategoryRequest>
{
    public GetProductValidator()
    {

    }
}