using FluentValidation;

namespace fastfood_products.Application.UseCases.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(dto => dto.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PBE002")
            .Length(3, 255)
            .WithMessage("PBE003");

        RuleFor(dto => dto.Type)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PBE004")
            .IsInEnum()
            .WithMessage("PBE005");

        RuleFor(dto => dto.Price)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PBE006");

        RuleFor(dto => dto.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PBE007")
            .Length(3, 255)
            .WithMessage("PBE008");

        RuleFor(dto => dto.ProductImageUrl)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PBE009");
    }
}
