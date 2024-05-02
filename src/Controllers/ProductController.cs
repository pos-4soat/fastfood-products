using fastfood_products.Controllers.Base;
using fastfood_products.Interface;
using fastfood_products.Models.Base;
using fastfood_products.Models.Request;
using fastfood_products.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace fastfood_products.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{ver:apiVersion}/[controller]")]
public class ProductController(IProductService _service) : BaseController
{
    [HttpGet]
    [SwaggerOperation(Summary = "List all products")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<>))]
    //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Error handled by the application", typeof(TopupCreateErrorResponse))]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        Result<GetProductResponse> result = await _service.GetAllAsync(cancellationToken);
        IActionResult response = await GetResponseFromResult(result);
        return response;
    }

    //[HttpGet("{productId}")]
    //[SwaggerOperation(Summary = "Retrieve product")]
    //[SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<>))]
    ////[SwaggerResponse((int)HttpStatusCode.BadRequest, "Error handled by the application", typeof(TopupCreateErrorResponse))]
    //[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Access Denied", typeof(ErrorResponse<Error>))]
    //[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unknown error", typeof(ErrorResponse<Error>))]
    //public async Task<IActionResult> GetAsync(int productId, CancellationToken cancellationToken)
    //{
    //    //var result = await _service.CreateAsync(createProductRequest, cancellationToken);
    //    //IActionResult response = await GetResponseFromResultAsync(result, cancellationToken);
    //    //return response;
    //}

    //[HttpGet("category/{type}")]
    //[SwaggerOperation(Summary = "List products by category")]
    //[SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<>))]
    ////[SwaggerResponse((int)HttpStatusCode.BadRequest, "Error handled by the application", typeof(TopupCreateErrorResponse))]
    //[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Access Denied", typeof(ErrorResponse<Error>))]
    //[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unknown error", typeof(ErrorResponse<Error>))]
    //public async Task<IActionResult> GetByTypeAsync(int type, CancellationToken cancellationToken)
    //{
    //    //var result = await _service.CreateAsync(createProductRequest, cancellationToken);
    //    //IActionResult response = await GetResponseFromResultAsync(result, cancellationToken);
    //    //return response;
    //}

    [HttpPost]
    [SwaggerOperation(Summary = "Create a product")]
    [SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<CreateProductResponse>))]
    //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Error handled by the application", typeof(TopupCreateErrorResponse))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequest createProductRequest, CancellationToken cancellationToken)
    {
        Result<CreateProductResponse> result = await _service.CreateAsync(createProductRequest, cancellationToken);
        IActionResult response = await GetResponseFromResult(result);
        return response;
    }

    //[HttpPut("{productId}")]
    //[SwaggerOperation(Summary = "Edit a product")]
    //[SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<>))]
    ////[SwaggerResponse((int)HttpStatusCode.BadRequest, "Error handled by the application", typeof(TopupCreateErrorResponse))]
    //[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Access Denied", typeof(ErrorResponse<Error>))]
    //[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unknown error", typeof(ErrorResponse<Error>))]
    //public async Task<IActionResult> EditAsync(int productId, [FromBody] CreateProductRequest createProductRequest, CancellationToken cancellationToken)
    //{
    //    //var result = await _service.CreateAsync(createProductRequest, cancellationToken);
    //    //IActionResult response = await GetResponseFromResultAsync(result, cancellationToken);
    //    //return response;
    //}

    //[HttpDelete("{productId}")]
    //[SwaggerOperation(Summary = "Delete a product")]
    //[SwaggerResponse((int)HttpStatusCode.OK, "OK", typeof(Response<>))]
    ////[SwaggerResponse((int)HttpStatusCode.BadRequest, "Error handled by the application", typeof(TopupCreateErrorResponse))]
    //[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Access Denied", typeof(ErrorResponse<Error>))]
    //[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Unknown error", typeof(ErrorResponse<Error>))]
    //public async Task<IActionResult> CreateAsync(int productId, CancellationToken cancellationToken)
    //{
    //    //var result = await _service.CreateAsync(createProductRequest, cancellationToken);
    //    //IActionResult response = await GetResponseFromResultAsync(result, cancellationToken);
    //    //return response;
    //}
}
