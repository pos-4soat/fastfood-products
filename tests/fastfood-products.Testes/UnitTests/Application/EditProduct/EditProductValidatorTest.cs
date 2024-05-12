using fastfood_products.Application.UseCases.CreateProduct;
using fastfood_products.Application.UseCases.EditProduct;
using fastfood_products.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var request = _modelFakerFactory.EditProductRequestWithId();

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE011");
    }

    [Test]
    public void ShouldValidateNameRequirement()
    {
        var request = _modelFakerFactory.EditProductRequestWithName(string.Empty);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE002");
    }

    [Test]
    public void ShouldValidateNameLenght()
    {
        var request = _modelFakerFactory.EditProductRequestWithName("Ab");

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE003");
    }

    [Test]
    public void ShouldValidateCategoryRequirement()
    {
        var request = _modelFakerFactory.EditProductRequestWithType(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE004");
    }

    [Test]
    public void ShouldValidateCategoryEnum()
    {
        var request = _modelFakerFactory.EditProductRequestWithType((CategoryType)999);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE005");
    }

    [Test]
    public void ShouldValidatePriceRequirement()
    {
        var request = _modelFakerFactory.EditProductRequestWithPrice(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE006");
    }

    [Test]
    public void ShouldValidateDescriptionRequirement()
    {
        var request = _modelFakerFactory.EditProductRequestWithDescription(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE007");
    }

    [Test]
    public void ShouldValidateDescriptionLenght()
    {
        var request = _modelFakerFactory.EditProductRequestWithDescription("Ab");

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE008");
    }

    [Test]
    public void ShouldValidateImageRequirement()
    {
        var request = _modelFakerFactory.EditProductRequestWithImage(null);

        var result = _validator.Validate(request);

        AssertExtensions.AssertValidation(result, "PBE009");
    }
}
