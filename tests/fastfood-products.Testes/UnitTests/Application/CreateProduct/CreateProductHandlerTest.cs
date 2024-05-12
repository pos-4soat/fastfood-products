using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.CreateProduct;
using Moq;
using System.Net;

namespace fastfood_products.Testes.UnitTests.Application.CreateProduct;

public class CreateProductHandlerTest : TestFixture
{
    [Test, Description("Should return product created successfully")]
    public async Task ShouldCreateProductAsync()
    {
        CreateProductRequest request = _modelFakerFactory.CreateProductRequest();

        CreateProductHandler service = new(_repositoryMock.Object, _mapper);

        Result<CreateProductResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result, HttpStatusCode.Created);

        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(result.Value.Description, Is.EqualTo(request.Description));
        Assert.That(result.Value.Price, Is.EqualTo(request.Price));
        Assert.That(result.Value.ProductImageUrl, Is.EqualTo(request.ProductImageUrl));
        Assert.That(result.Value.Type, Is.EqualTo(request.Type));

        _repositoryMock.VerifyCreateProductAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}
