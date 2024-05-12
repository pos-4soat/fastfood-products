using AutoMapper;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Contracts.Repository;
using MediatR;

namespace fastfood_products.Application.UseCases.GetProductByCategory;

public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategoryRequest, Result<GetProductByCategoryResponse>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByCategoryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
    }

    public async Task<Result<GetProductByCategoryResponse>> Handle(GetProductByCategoryRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entity.ProductEntity> products = await _productRepository.GetProductsByCategoryAsync(request.Type, cancellationToken);

        IEnumerable<GetProductByCategoryProductData> productsResponse = _mapper.Map<IEnumerable<GetProductByCategoryProductData>>(products);

        return Result<GetProductByCategoryResponse>.Success(new GetProductByCategoryResponse(productsResponse));
    }
}
