using fastfood_products.Models.Base;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;

namespace fastfood_products.Handlers;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(
                new ErrorResponse<Error>(new Error("999", "Internal server error"))
            ));
        }
    }
}
