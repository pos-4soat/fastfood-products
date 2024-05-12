using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.EditProduct;
using fastfood_products.Domain.Entity;
using Moq;
using System.Net;

namespace fastfood_products.Testes.UnitTests.Application.EditProduct;

public class EditProductHandlerTest : TestFixture
{
    private EditProductHandler _service;

    [SetUp]
    public void EditProductHandlerTestSetup()
    {
        _service = new(_repositoryMock.Object, _mapper);
    }

    [Test, Description("Should return product updated successfully")]
    public async Task ShouldUpdateProductAsync()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequest();
        ProductEntity entity = _modelFakerFactory.ProductEntity(request.Id);

        _repositoryMock.SetupGetProductByIdAsync(entity);

        Result<EditProductResponse> result = await _service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        Assert.That(result.Value.Name, Is.EqualTo(request.Name));
        Assert.That(result.Value.Description, Is.EqualTo(request.Description));
        Assert.That(result.Value.Price, Is.EqualTo(request.Price));
        Assert.That(result.Value.ProductImageUrl, Is.EqualTo(request.ProductImageUrl));
        Assert.That(result.Value.Type, Is.EqualTo(request.Type));

        _repositoryMock.VerifyGetProductByIdAsync(request.Id, Times.Once());
        _repositoryMock.VerifyUpdateProductAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return product not found with specified parameters")]
    public async Task ShouldReturnProductNotFound()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequest();

        _repositoryMock.SetupGetProductByIdAsync(null);

        Result<EditProductResponse> result = await _service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE010", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductByIdAsync(request.Id, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}
