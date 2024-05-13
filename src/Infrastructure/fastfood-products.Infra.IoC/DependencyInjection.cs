using fastfood_products.Application.Shared.BaseResponse;
using fastfood_products.Domain.Contracts.Repository;
using fastfood_products.Infra.SqlServer.Context;
using fastfood_products.Infra.SqlServer.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace fastfood_products.Infra.IoC;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBehavior();
        services.ConfigureServices();
        services.ConfigureAutomapper();
        services.ConfigureMediatr();
        services.ConfigureFluentValidation();
        services.ConfigureDatabase(configuration);
    }

    private static void ConfigureBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
    }
    private static void ConfigureAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Result).Assembly);
    }

    private static void ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Result).Assembly));
    }

    private static void ConfigureFluentValidation(this IServiceCollection service)
    {
        service.AddValidatorsFromAssemblyContaining<Result>();
        service.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        service.AddFluentValidationRulesToSwagger();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                                                     b => b.MigrationsAssembly("fastfood-products.Infra.SqlServer")).LogTo(s => Console.WriteLine(s)));
    }
}
