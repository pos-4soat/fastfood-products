using AutoMapper;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Domain.Entity;
using fastfood_products.Domain.Enum;
using MediatR;

namespace fastfood_products.Application.UseCases.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, Result<CreateProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }

    public async Task<Result<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        ProductEntity product = _mapper.Map<ProductEntity>(request);
        await _productRepository.CreateProductAsync(product, cancellationToken);

        return Result<CreateProductResponse>.Success(_mapper.Map<CreateProductResponse>(product), StatusResponse.CREATED);
    }
}
