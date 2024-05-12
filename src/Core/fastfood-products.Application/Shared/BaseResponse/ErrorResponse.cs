using fastfood_products.Domain.Enum;
using Newtonsoft.Json;

namespace fastfood_products.Application.Shared.BaseResponse;

public class ErrorResponse<TBody> : BaseResponse where TBody : class
{
    [JsonProperty(Order = 3)]
    public TBody? Error { get; set; }

    public ErrorResponse(TBody body) : base(StatusResponse.ERROR) => Error = body;
}