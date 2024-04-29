﻿using fastfood_products.Data.Context;
using fastfood_products.Data.Repository;
using fastfood_products.Interface;
using fastfood_products.Services;
using Microsoft.EntityFrameworkCore;

namespace fastfood_products.IoC;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureServices();
        services.ConfigureDatabase(configuration);
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        _ = services.AddTransient<IProductService, ProductService>();
        _ = services.AddScoped<IProductRepository, ProductRepository>();
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                                                     b => b.MigrationsAssembly("fastfood-products")));
    }
}
