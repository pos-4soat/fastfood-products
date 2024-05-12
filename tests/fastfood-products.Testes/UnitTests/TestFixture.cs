using AutoFixture;
using AutoMapper;
using Bogus;
using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Testes.Mocks;
using Moq;
using Moq.AutoMock;

namespace fastfood_products.Testes.UnitTests;

[TestFixture]
public abstract class TestFixture
{
    public Fixture AutoFixture { get; init; } = new();
    public Faker Faker { get; init; } = new();

    protected ProductRepositoryMock _repositoryMock;
    protected IMapper _mapper;

    protected ModelFakerFactory _modelFakerFactory;
    protected AutoMocker _autoMocker;

    [OneTimeSetUp]
    public void GlobalPrepare()
    {
        _autoMocker = new();
        _modelFakerFactory = new(AutoFixture, Faker);
    }

    [SetUp]
    public async Task SetUpAsync()
    {
        AddCustomMocksToContainer();
        InstantiateCustomMocks();
        CreateMapper();
    }

    [TearDown]
    public void TearDown()
    {
        foreach (KeyValuePair<Type, object?> resolvedObject in _autoMocker.ResolvedObjects)
            (resolvedObject.Value as Mock)?.Invocations.Clear();
    }

    protected T CreateInstance<T>() where T : class
        => _autoMocker.CreateInstance<T>();

    protected TCustomMock GetCustomMock<TInterface, TCustomMock>() where TCustomMock : BaseCustomMock<TInterface> where TInterface : class
        => (_autoMocker.GetMock<TInterface>() as TCustomMock)!;

    protected Mock<T> GetMock<T>() where T : class
        => _autoMocker.GetMock<T>();

    #region Private Methods

    private void AddCustomMocksToContainer()
    {
        _autoMocker.Use(new ProductRepositoryMock(this).ConvertToBaseType());
    }

    private void InstantiateCustomMocks()
    {
        _repositoryMock = GetCustomMock<IProductRepository, ProductRepositoryMock>();
    }

    private void CreateMapper()
    {
        MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Result).Assembly));

        _mapper = config.CreateMapper();
    }
    #endregion Private Methods
}
