using fastfood_products.API.Controllers;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.DeleteProduct;
using fastfood_products.Application.UseCases.EditProduct;
using fastfood_products.Application.UseCases.GetProduct;
using fastfood_products.Application.UseCases.GetProductByCategory;
using fastfood_products.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace fastfood_products.Testes.UnitTests.Controller;

public class ProductControllerTest : TestFixture
{
    [Test, Description("")]
    public async Task ShouldUpdateProductAsync()
    {
        EditProductRequest request = _modelFakerFactory.EditProductRequest();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<EditProductRequest>(), default))
            .ReturnsAsync(Result<EditProductResponse>.Success(new EditProductResponse()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ProductImageUrl = request.ProductImageUrl,
                Type = request.Type,
                Id = request.Id
            }));

        ProductController service = new(_mediatorMock.Object);

        IActionResult result = await service.UpdateProductAsync(request, default);

        AssertExtensions.AssertResponse<EditProductRequest, EditProductResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), request);
    }

    [Test, Description("")]
    public async Task ShouldDeleteProductAsync()
    {
        DeleteProductRequest request = _modelFakerFactory.DeleteProductRequest();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteProductRequest>(), default))
            .ReturnsAsync(Result<DeleteProductResponse>.Success(new DeleteProductResponse()));

        ProductController service = new(_mediatorMock.Object);

        IActionResult result = await service.DeleteProductAsync(request.ProductId, default);

        AssertExtensions.AssertResponse<DeleteProductRequest, DeleteProductResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), request);
    }

    [Test, Description("")]
    public async Task ShouldGetProductByCategoryAsync()
    {
        GetProductByCategoryRequest request = _modelFakerFactory.GetProductByCategoryRequest();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductByCategoryRequest>(), default))
            .ReturnsAsync(Result<GetProductByCategoryResponse>.Success(new GetProductByCategoryResponse(_modelFakerFactory.GetProductByCategoryProductData(CategoryType.Burguer))));

        ProductController service = new(_mediatorMock.Object);

        IActionResult result = await service.GetProductByCategoryAsync(request.Type, default);

        AssertExtensions.AssertResponse<GetProductByCategoryRequest, GetProductByCategoryResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), request);
    }

    [Test, Description("")]
    public async Task ShouldGetProductAsync()
    {
        GetProductRequest request = _modelFakerFactory.GetProductRequest();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetProductRequest>(), default))
            .ReturnsAsync(Result<GetProductResponse>.Success(new GetProductResponse()));

        ProductController service = new(_mediatorMock.Object);

        IActionResult result = await service.GetProductAsync(request.ProductId, default);

        AssertExtensions.AssertResponse<GetProductRequest, GetProductResponse>(result, HttpStatusCode.OK, nameof(StatusResponse.SUCCESS), request);
    }


    [Test, Description("")]
    public async Task ShouldReturnProductNotFoundOnDeleteProductAsync()
    {
        DeleteProductRequest request = _modelFakerFactory.DeleteProductRequest();

        Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteProductRequest>(), default))
            .ReturnsAsync(Result<DeleteProductResponse>.Failure("PBE010"));

        ProductController service = new(_mediatorMock.Object);

        IActionResult result = await service.DeleteProductAsync(request.ProductId, default);

        AssertExtensions.AssertErrorResponse(result, HttpStatusCode.BadRequest, nameof(StatusResponse.ERROR));
    }
}
