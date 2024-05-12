using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Domain.Entity;
using fastfood_products.Domain.Enum;
using fastfood_products.Testes.UnitTests;
using Moq;

namespace fastfood_products.Testes.Mocks;

public class ProductRepositoryMock : BaseCustomMock<IProductRepository>
{
    public ProductRepositoryMock(TestFixture testFixture) : base(testFixture)
    {
        SetupCreateProductAsync();
        SetupDeleteProductAsync();
        SetupUpdateProductAsync();
    }

    public void SetupCreateProductAsync()
        => Setup(x => x.CreateProductAsync(It.IsAny<ProductEntity>(), default))
            .Returns(Task.CompletedTask);

    public void SetupDeleteProductAsync()
        => Setup(x => x.DeleteProductAsync(It.IsAny<ProductEntity>(), default))
            .Returns(Task.CompletedTask);

    public void SetupUpdateProductAsync()
        => Setup(x => x.UpdateProductAsync(It.IsAny<ProductEntity>(), default))
            .Returns(Task.CompletedTask);

    public void SetupGetProductByIdAsync(ProductEntity expectedReturn)
        => Setup(x => x.GetProductByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(expectedReturn);

    public void SetupGetProductsByCategoryAsync(IEnumerable<ProductEntity> expectedReturn)
        => Setup(x => x.GetProductsByCategoryAsync(It.IsAny<CategoryType>(), default))
            .ReturnsAsync(expectedReturn);

    public void VerifyCreateProductAsync(Times? times = null)
        => Verify(x => x.CreateProductAsync(It.IsAny<ProductEntity>(), default), times ?? Times.Once());

    public void VerifyDeleteProductAsync(Times? times = null)
        => Verify(x => x.DeleteProductAsync(It.IsAny<ProductEntity>(), default), times ?? Times.Once());

    public void VerifyUpdateProductAsync(Times? times = null)
        => Verify(x => x.UpdateProductAsync(It.IsAny<ProductEntity>(), default), times ?? Times.Once());

    public void VerifyGetProductByIdAsync(int productId, Times? times = null)
        => Verify(x => x.GetProductByIdAsync(productId, default), times ?? Times.Once());

    public void VerifyGetProductsByCategoryAsync(CategoryType type, Times? times = null)
        => Verify(x => x.GetProductsByCategoryAsync(type, default), times ?? Times.Once());
}
