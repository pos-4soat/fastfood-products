using fastfood_products.Constants;
using Newtonsoft.Json;

namespace fastfood_products.Models.Base;

public class ErrorResponse<TBody> : BaseResponse where TBody : class
{
    [JsonProperty(Order = 3)]
    public TBody? Error { get; set; }

    public ErrorResponse(TBody body) : base(StatusEnum.ERROR) => Error = body;
}