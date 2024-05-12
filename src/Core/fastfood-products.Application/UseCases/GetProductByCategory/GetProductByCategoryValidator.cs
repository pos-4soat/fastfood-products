using FluentValidation;

namespace fastfood_products.Application.UseCases.GetProductByCategory;

public class GetProductByCategoryValidator : AbstractValidator<GetProductByCategoryRequest>
{
    public GetProductByCategoryValidator()
    {

    }
}
