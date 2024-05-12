using AutoMapper;
using fastfood_products.Domain.Entity;

namespace fastfood_products.Application.UseCases.GetProduct;

public class GetProductMapper : Profile
{
    public GetProductMapper()
    {
        CreateMap<ProductEntity, GetProductResponse>();
    }
}