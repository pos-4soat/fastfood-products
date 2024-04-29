using fastfood_products.Constants;
using fastfood_products.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace fastfood_products.Controllers.Base;

[ApiController]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    public BaseController()
    {
    }

    protected async ValueTask<IActionResult> GetResponseFromResultAsync<TValue>(
        Result<TValue> result, CancellationToken cancellationToken)
    {
        if (result.IsFailure)
        {
            ErrorResponse<BaseResponse> errorResponse = await CreateErrorResponseFromResult(result, cancellationToken);

            return StatusCode((int)result.StatusCode, errorResponse);
        }

        Response<object> response = new Response<object>(result.Value!, result.Status);

        return StatusCode((int)HttpStatusCode.OK, response);
    }

    protected async ValueTask<IActionResult> GetResponseFromResultAsync(
       Result result, CancellationToken cancellationToken)
    {
        if (result.IsFailure)
        {
            ErrorResponse<BaseResponse> errorResponse = await CreateErrorResponseFromResult(result, cancellationToken);

            return StatusCode((int)result.StatusCode, errorResponse);
        }

        return StatusCode((int)HttpStatusCode.OK);
    }

    private async Task<ErrorResponse<BaseResponse>> CreateErrorResponseFromResult(Result result, CancellationToken cancellationToken)
    {
        //var errorReponse = _mapper.Map<ErrorResponse>(error);
        BaseResponse obj = new BaseResponse(StatusEnum.ERROR);
        return new ErrorResponse<BaseResponse>(obj!);
    }
}
