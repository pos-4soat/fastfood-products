using AutoMapper;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Domain.Entity;
using MediatR;

namespace fastfood_products.Application.UseCases.EditProduct;

public class EditProductHandler : IRequestHandler<EditProductRequest, Result<EditProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public EditProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }

    public async Task<Result<EditProductResponse>> Handle(EditProductRequest request, CancellationToken cancellationToken)
    {
        ProductEntity product = _mapper.Map<ProductEntity>(request);

        ProductEntity? existentProduct = await _productRepository.GetProductByIdAsync(product.Id, cancellationToken);

        if (existentProduct == null)
            return Result<EditProductResponse>.Failure("PBE010");

        existentProduct.Price = product.Price;
        existentProduct.Name = product.Name;
        existentProduct.Type = product.Type;
        existentProduct.ProductImageUrl = product.ProductImageUrl;
        existentProduct.Description = product.Description;

        await _productRepository.UpdateProductAsync(existentProduct, cancellationToken);

        return Result<EditProductResponse>.Success(_mapper.Map<EditProductResponse>(existentProduct));
    }
}
