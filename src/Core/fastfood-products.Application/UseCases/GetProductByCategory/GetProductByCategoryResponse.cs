using fastfood_products.Domain.Enum;

namespace fastfood_products.Application.UseCases.GetProductByCategory;

public sealed record GetProductByCategoryResponse
{
    public IEnumerable<GetProductByCategoryProductData> Products { get; set; }

    public GetProductByCategoryResponse(IEnumerable<GetProductByCategoryProductData> productsResponse)
    {
        Products = productsResponse;
    }
}

public sealed record GetProductByCategoryProductData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ProductImageUrl { get; set; }
}
