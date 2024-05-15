using AutoMapper;
using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Infra.IoC;
using fastfood_products.Infra.SqlServer.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.AutoMock;

namespace fastfood_products.Testes.UnitTests.Infrastructure.IoC
{
    public class DependencyInjectionTest
    {
        private AutoMocker _mocker;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
        }

        [Test]
        public void RegisterServices_Should_ConfigureServicesCorrectly()
        {
            ServiceCollection services = _mocker.CreateInstance<ServiceCollection>();

            DependencyInjection.RegisterServices(services, new Mock<IConfiguration>().Object);

            Assert.That(services.Any(s => s.ServiceType == typeof(IProductRepository)), Is.True);
            Assert.That(services.Any(s => s.ServiceType == typeof(IMapper)), Is.True);
            Assert.That(services.Any(s => s.ServiceType == typeof(IMediator)), Is.True);
            Assert.That(services.Any(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)), Is.True);
        }

        [Test]
        public void RegisterServices_Should_ConfigureAutomapperCorrectly()
        {
            ServiceCollection services = _mocker.CreateInstance<ServiceCollection>();

            DependencyInjection.RegisterServices(services, new Mock<IConfiguration>().Object);

            IMapper? mapper = services.BuildServiceProvider().GetService<IMapper>();
            Assert.That(mapper, Is.Not.Null);
        }

        [Test]
        public void RegisterServices_Should_ConfigureMediatrCorrectly()
        {
            ServiceCollection services = _mocker.CreateInstance<ServiceCollection>();

            DependencyInjection.RegisterServices(services, new Mock<IConfiguration>().Object);

            IMediator? mediator = services.BuildServiceProvider().GetService<IMediator>();
            Assert.That(mediator, Is.Not.Null);
        }
    }
}
