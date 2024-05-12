using AutoMapper;
using fastfood_products.Domain.Entity;

namespace fastfood_products.Application.UseCases.GetProductByCategory;

public class GetProductByCategoryMapper : Profile
{
    public GetProductByCategoryMapper()
    {
        CreateMap<ProductEntity, GetProductByCategoryProductData>();
    }
}
