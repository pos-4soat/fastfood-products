using fastfood_products.Application.UseCases.CreateProduct;
using fastfood_products.Domain.Enum;

namespace fastfood_products.Testes.UnitTests.Application.CreateProduct;

public class CreateProductValidatorTest : TestFixture
{
    private CreateProductValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new CreateProductValidator();
    }

    [Test]
    public void ShouldValidateRequirement()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithName(string.Empty);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE002");
    }

    [Test]
    public void ShouldValidateNameLenght()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithName("Ab");

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE003");
    }

    [Test]
    public void ShouldValidateCategoryRequirement()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithType(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE004");
    }

    [Test]
    public void ShouldValidateCategoryEnum()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithType((CategoryType)999);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE005");
    }

    [Test]
    public void ShouldValidatePriceRequirement()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithPrice(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE006");
    }

    [Test]
    public void ShouldValidateDescriptionRequirement()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithDescription(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE007");
    }

    [Test]
    public void ShouldValidateDescriptionLenght()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithDescription("Ab");

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE008");
    }

    [Test]
    public void ShouldValidateImageRequirement()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequestWithImage(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE009");
    }
}
