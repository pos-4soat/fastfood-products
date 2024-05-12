using fastfood_products.Domain.Enum;

namespace fastfood_products.Application.UseCases.EditProduct;

public sealed record EditProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ProductImageUrl { get; set; }
}
