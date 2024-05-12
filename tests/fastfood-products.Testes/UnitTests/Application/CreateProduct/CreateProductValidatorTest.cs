using fastfood_products.Application.UseCases.CreateProduct;
using fastfood_products.Application.UseCases.EditProduct;
using fastfood_products.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NUnit.Framework.Internal.OSPlatform;

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
        var request = _modelFakerFactory.CreateProductRequestWithName(string.Empty);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE002");
    }

    [Test]
    public void ShouldValidateNameLenght()
    {
        var request = _modelFakerFactory.CreateProductRequestWithName("Ab");

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE003");
    }

    [Test]
    public void ShouldValidateCategoryRequirement()
    {
        var request = _modelFakerFactory.CreateProductRequestWithType(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE004");
    }

    [Test]
    public void ShouldValidateCategoryEnum()
    {
        var request = _modelFakerFactory.CreateProductRequestWithType((CategoryType)999);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE005");
    }

    [Test]
    public void ShouldValidatePriceRequirement()
    {
        var request = _modelFakerFactory.CreateProductRequestWithPrice(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE006");
    }

    [Test]
    public void ShouldValidateDescriptionRequirement()
    {
        var request = _modelFakerFactory.CreateProductRequestWithDescription(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE007");
    }

    [Test]
    public void ShouldValidateDescriptionLenght()
    {
        var request = _modelFakerFactory.CreateProductRequestWithDescription("Ab");

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE008");
    }

    [Test]
    public void ShouldValidateImageRequirement()
    {
        var request = _modelFakerFactory.CreateProductRequestWithImage(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE009");
    }
}
