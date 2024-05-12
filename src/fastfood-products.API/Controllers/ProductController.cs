using fastfood_products.API.Controllers.Base;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Application.UseCases.CreateProduct;
using fastfood_products.Application.UseCases.DeleteProduct;
using fastfood_products.Application.UseCases.EditProduct;
using fastfood_products.Application.UseCases.GetProduct;
using fastfood_products.Application.UseCases.GetProductByCategory;
using fastfood_products.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace fastfood_products.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{ver:apiVersion}/[controller]")]
public class ProductController(IMediator _mediator) : BaseController
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create a product")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<CreateProductResponse>>))]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductRequest createProductRequest, CancellationToken cancellationToken)
    {
        Result<CreateProductResponse> result = await _mediator.Send(createProductRequest, cancellationToken);
        return await GetResponseFromResult(result);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Edit a product")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<EditProductResponse>>))]
    public async Task<IActionResult> UpdateProductAsync([FromBody] EditProductRequest editProductRequest, CancellationToken cancellationToken)
    {
        Result<EditProductResponse> result = await _mediator.Send(editProductRequest, cancellationToken);
        return await GetResponseFromResult(result);
    }

    [HttpDelete("{productId}")]
    [SwaggerOperation(Summary = "Delete a product")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<DeleteProductResponse>>))]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] int productId, CancellationToken cancellationToken)
    {
        Result<DeleteProductResponse> result = await _mediator.Send(new DeleteProductRequest(productId), cancellationToken);
        return await GetResponseFromResult(result);
    }

    [HttpGet("category/{type}")]
    [SwaggerOperation(Summary = "List all products by category")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<GetProductByCategoryResponse>>))]
    public async Task<IActionResult> GetProductByCategoryAsync([FromRoute] CategoryType type, CancellationToken cancellationToken)
    {
        Result<GetProductByCategoryResponse> result = await _mediator.Send(new GetProductByCategoryRequest(type), cancellationToken);
        return await GetResponseFromResult(result);
    }

    [HttpGet("{productId}")]
    [SwaggerOperation(Summary = "Get product")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<Result<GetProductResponse>>))]
    public async Task<IActionResult> GetProductAsync([FromRoute] int productId, CancellationToken cancellationToken)
    {
        Result<GetProductResponse> result = await _mediator.Send(new GetProductRequest(productId), cancellationToken);
        return await GetResponseFromResult(result);
    }
}
