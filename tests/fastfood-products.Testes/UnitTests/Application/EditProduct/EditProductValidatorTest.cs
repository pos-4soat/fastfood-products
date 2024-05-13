using fastfood_products.Application.UseCases.EditProduct;
using fastfood_products.Domain.Enum;

namespace fastfood_products.Testes.UnitTests.Application.EditProduct;

public class EditProductValidatorTest : TestFixture
{
    private EditProductValidator _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new EditProductValidator();
    }

    [Test]
    public void ShouldValidateIdRequirement()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithId();

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE011");
    }

    [Test]
    public void ShouldValidateNameRequirement()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithName(string.Empty);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE002");
    }

    [Test]
    public void ShouldValidateNameLenght()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithName("Ab");

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE003");
    }

    [Test]
    public void ShouldValidateCategoryRequirement()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithType(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE004");
    }

    [Test]
    public void ShouldValidateCategoryEnum()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithType((CategoryType)999);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE005");
    }

    [Test]
    public void ShouldValidatePriceRequirement()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithPrice(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE006");
    }

    [Test]
    public void ShouldValidateDescriptionRequirement()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithDescription(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE007");
    }

    [Test]
    public void ShouldValidateDescriptionLenght()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithDescription("Ab");

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE008");
    }

    [Test]
    public void ShouldValidateImageRequirement()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequestWithImage(null);

        FluentValidation.Results.ValidationResult result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE009");
    }
}
