using fastfood_products.Constants;
using Newtonsoft.Json;

namespace fastfood_products.Models.Base;

public class Response<TBody> : BaseResponse where TBody : class
{
    [JsonProperty(Order = 3)]
    public TBody? Body { get; set; }

    public Response(TBody body, StatusEnum status) : base(status) => Body = body;
}
