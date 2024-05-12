using AutoMapper;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Domain.Entity;
using MediatR;

namespace fastfood_products.Application.UseCases.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductRequest, Result<GetProductResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }

    public async Task<Result<GetProductResponse>> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        ProductEntity product = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

        if (product == null)
            return Result<GetProductResponse>.Failure("PBE010");

        GetProductResponse productResponse = _mapper.Map<GetProductResponse>(product);

        return Result<GetProductResponse>.Success(productResponse);
    }
}
