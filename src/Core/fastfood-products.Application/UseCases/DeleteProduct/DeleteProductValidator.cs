using FluentValidation;

namespace fastfood_products.Application.UseCases.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductRequest>
{
    public DeleteProductValidator()
    {

    }
}
