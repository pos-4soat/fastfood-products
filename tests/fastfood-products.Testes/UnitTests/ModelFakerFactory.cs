using AutoFixture;
using Bogus;
using fastfood_products.Application.UseCases.CreateProduct;
using fastfood_products.Application.UseCases.DeleteProduct;
using fastfood_products.Application.UseCases.EditProduct;
using fastfood_products.Application.UseCases.GetProduct;
using fastfood_products.Application.UseCases.GetProductByCategory;
using fastfood_products.Domain.Entity;
using fastfood_products.Domain.Enum;

namespace fastfood_products.Testes.UnitTests;

public class ModelFakerFactory(Fixture autoFixture, Faker faker)
{
    private readonly Fixture _autoFixture = autoFixture;
    private readonly Faker _faker = faker;

    #region CreateProduct
    public CreateProductRequest CreateProductRequest()
        => _autoFixture.Build<CreateProductRequest>()
            .Create();

    public CreateProductRequest CreateProductRequestWithName(string name)
        => _autoFixture.Build<CreateProductRequest>()
            .With(c => c.Name, name)
            .Create();

    public CreateProductRequest CreateProductRequestWithType(CategoryType? type)
        => _autoFixture.Build<CreateProductRequest>()
            .With(c => c.Type, type)
            .Create();

    public CreateProductRequest CreateProductRequestWithPrice(decimal? price)
        => _autoFixture.Build<CreateProductRequest>()
            .With(c => c.Price, price)
            .Create();

    public CreateProductRequest CreateProductRequestWithDescription(string description)
        => _autoFixture.Build<CreateProductRequest>()
            .With(c => c.Description, description)
            .Create();

    public CreateProductRequest CreateProductRequestWithImage(string image)
        => _autoFixture.Build<CreateProductRequest>()
            .With(c => c.ProductImageUrl, image)
            .Create();
    #endregion

    public DeleteProductRequest DeleteProductRequest()
        => _autoFixture.Build<DeleteProductRequest>()
            .Create();

    #region EditProduct
    public EditProductRequest EditProductRequest()
        => _autoFixture.Build<EditProductRequest>()
            .Create();

    public EditProductRequest EditProductRequestWithId()
        => _autoFixture.Build<EditProductRequest>()
            .With(c => c.Id, default(int?))
            .Without(c => c.Id)
            .Create();

    public EditProductRequest EditProductRequestWithName(string name)
        => _autoFixture.Build<EditProductRequest>()
            .With(c => c.Name, name)
            .Create();

    public EditProductRequest EditProductRequestWithType(CategoryType? type)
        => _autoFixture.Build<EditProductRequest>()
            .With(c => c.Type, type)
            .Create();

    public EditProductRequest EditProductRequestWithPrice(decimal? price)
        => _autoFixture.Build<EditProductRequest>()
            .With(c => c.Price, price)
            .Create();

    public EditProductRequest EditProductRequestWithDescription(string description)
        => _autoFixture.Build<EditProductRequest>()
            .With(c => c.Description, description)
            .Create();

    public EditProductRequest EditProductRequestWithImage(string image)
        => _autoFixture.Build<EditProductRequest>()
            .With(c => c.ProductImageUrl, image)
            .Create();
    #endregion

    public GetProductRequest GetProductRequest()
        => _autoFixture.Build<GetProductRequest>()
            .Create();

    public GetProductByCategoryRequest GetProductByCategoryRequest()
        => _autoFixture.Build<GetProductByCategoryRequest>()
            .Create();

    public ProductEntity ProductEntity(int productId)
        => _autoFixture.Build<ProductEntity>()
            .With(x => x.Id, productId)
            .Create();

    public IEnumerable<ProductEntity> ProductEntity(CategoryType type)
    {
        IEnumerable<ProductEntity> products = _autoFixture.CreateMany<ProductEntity>();
        foreach (ProductEntity product in products)
        {
            product.Type = type;
        }
        return products;
    }

    public IEnumerable<GetProductByCategoryProductData> GetProductByCategoryProductData(CategoryType type)
    {
        IEnumerable<GetProductByCategoryProductData> products = _autoFixture.CreateMany<GetProductByCategoryProductData>();
        foreach (GetProductByCategoryProductData product in products)
        {
            product.Type = type;
        }
        return products;
    }
}
