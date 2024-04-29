namespace fastfood_products.Models.Response;

public sealed record GetProductResponse
{
    public IEnumerable<ProductBaseResponse> Products { get; set; }

    public GetProductResponse(IEnumerable<ProductBaseResponse> products)
    {
        Products = products;
    }
}
