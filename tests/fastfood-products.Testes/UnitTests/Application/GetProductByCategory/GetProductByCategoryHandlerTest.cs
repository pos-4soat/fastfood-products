using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.GetProductByCategory;
using fastfood_products.Domain.Entity;
using Moq;

namespace fastfood_products.Testes.UnitTests.Application.GetProductByCategory;

public class GetProductByCategoryHandlerTest : TestFixture
{
    [Test, Description("Should return list of products by category")]
    public async Task ShouldListProductsByCategoryAsync()
    {
        GetProductByCategoryRequest request = _modelFakerFactory.GetProductByCategoryRequest();
        IEnumerable<ProductEntity> entity = _modelFakerFactory.ProductEntity(request.Type);

        _repositoryMock.SetupGetProductsByCategoryAsync(entity);

        GetProductByCategoryHandler service = new(_repositoryMock.Object, _mapper);

        Result<GetProductByCategoryResponse> result = await service.Handle(request, default);

        AssertExtensions.ResultIsSuccess(result);

        _repositoryMock.VerifyGetProductsByCategoryAsync(request.Type, Times.Once());
        _repositoryMock.VerifyNoOtherCalls();
    }
}