using fastfood_products.Constants;

namespace fastfood_products.Data.Entity;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public CategoryType Type { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string ProductImageUrl { get; set; }
}
