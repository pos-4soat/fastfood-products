using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.DeleteProduct;
using fastfood_products.Domain.Entity;
using Moq;
using System.Net;

namespace fastfood_products.Testes.UnitTests.Application.DeleteProduct;

public class DeleteProductHandlerTest : TestFixture
{
    private DeleteProductHandler _service;

    [SetUp]
    public void DeleteProductHandlerTestSetup()
    {
        _service = new(_repositoryMock.Object);
    }

    [Test, Description("Should return product deleted successfully")]
    public async Task ShouldDeleteProductAsync()
    {
        DeleteProductRequest request = _modelFakerFactory.DeleteProductRequest();
        ProductEntity entity = _modelFakerFactory.ProductEntity(request.ProductId);

        _repositoryMock.SetupGetProductByIdAsync(entity);

        Result<DeleteProductResponse> result = await _service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetProductByIdAsync(request.ProductId, Times.Once());
        _repositoryMock.VerifyDeleteProductAsync(Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return product not found with specified parameters")]
    public async Task ShouldReturnProductNotFound()
    {
        DeleteProductRequest request = _modelFakerFactory.DeleteProductRequest();

        _repositoryMock.SetupGetProductByIdAsync(null);

        Result<DeleteProductResponse> result = await _service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE010", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductByIdAsync(request.ProductId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}