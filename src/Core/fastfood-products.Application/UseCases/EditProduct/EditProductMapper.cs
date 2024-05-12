using AutoMapper;
using fastfood_products.Domain.Entity;

namespace fastfood_products.Application.UseCases.EditProduct;

internal class EditProductMapper : Profile
{
    public EditProductMapper()
    {
        CreateMap<EditProductRequest, ProductEntity>();
        CreateMap<ProductEntity, EditProductResponse>();
    }
}
