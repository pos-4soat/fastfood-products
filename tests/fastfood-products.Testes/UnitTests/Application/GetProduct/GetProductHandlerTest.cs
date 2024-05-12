using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.GetProduct;
using fastfood_products.Domain.Entity;
using Moq;
using System.Net;

namespace fastfood_products.Testes.UnitTests.Application.GetProduct;

public class GetProductHandlerTest : TestFixture
{
    private GetProductHandler _service;

    [SetUp]
    public void EditProductHandlerTestSetup()
    {
        _service = new(_repositoryMock.Object, _mapper);
    }

    [Test, Description("Should return product updated successfully")]
    public async Task ShouldUpdateProductAsync()
    {
        GetProductRequest request = _modelFakerFactory.GetProductRequest();
        ProductEntity entity = _modelFakerFactory.ProductEntity(request.ProductId);

        _repositoryMock.SetupGetProductByIdAsync(entity);

        Result<GetProductResponse> result = await _service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetProductByIdAsync(request.ProductId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }

    [Test, Description("Should return product not found with specified parameters")]
    public async Task ShouldReturnProductNotFound()
    {
        GetProductRequest request = _modelFakerFactory.GetProductRequest();

        _repositoryMock.SetupGetProductByIdAsync(null);

        Result<GetProductResponse> result = await _service.Handle(request, default);

        AssertExtensions.ResultIsFailure(result, "PBE010", HttpStatusCode.BadRequest);

        _repositoryMock.VerifyGetProductByIdAsync(request.ProductId, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}