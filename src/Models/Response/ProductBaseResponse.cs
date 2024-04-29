using fastfood_products.Constants;
using fastfood_products.Data.Entity;

namespace fastfood_products.Models.Response;

public class ProductBaseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ProductImageUrl { get; set; }

    public ProductBaseResponse(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Type = product.Type;
        Price = product.Price;
        Description = product.Description;
        ProductImageUrl = product.ProductImageUrl;
    }
}
