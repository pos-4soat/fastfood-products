using AutoMapper;
using fastfood_products.Domain.Entity;

namespace fastfood_products.Application.UseCases.CreateProduct;

public class CreateProductMapper : Profile
{
    public CreateProductMapper()
    {
        CreateMap<CreateProductRequest, ProductEntity>();
        CreateMap<ProductEntity, CreateProductResponse>();
    }
}
