using fastfood_products.Constants;

namespace fastfood_products.Models.Request;

public sealed record CreateProductRequest
    (string Name,
    CategoryType Type,
    decimal Price,
    string Description,
    string ProductImageUrl);
