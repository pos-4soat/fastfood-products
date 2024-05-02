using fastfood_products.Data.Context;
using fastfood_products.Data.Repository;
using fastfood_products.Interface;
using fastfood_products.Services;
using fastfood_products.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Text.Json;
using System.Text.Unicode;

namespace fastfood_products.IoC;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBehavior();
        services.ConfigureHealthCheck(configuration);
        services.ConfigureOptions();
        services.ConfigureSwagger();
        services.ConfigureVersion();
        services.ConfigureServices();
        services.ConfigureDatabase(configuration);
    }

    public static void ConfigureLogging(this ILoggingBuilder logging)
    {
        logging.ClearProviders();
        logging.AddConsole();
    }

    private static void ConfigureBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
    }

    private static void ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var conStr = configuration.GetConnectionString("SqlServerConnection");
        if (string.IsNullOrWhiteSpace(conStr))
            throw new InvalidOperationException(
                $"Could not find a connection string named 'SqlServerConnection'.");

        services
            .AddHealthChecks()
            .AddCheck<SimpleHealthCheck>("live", failureStatus: HealthStatus.Unhealthy, tags: new[] { "live" })
            .AddSqlServer(conStr);
    }

    private static void ConfigureFluentValidation(this IServiceCollection service)
    {
        //services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        //services.AddFluentValidationRulesToSwagger();
    }

    private static void ConfigureOptions(this IServiceCollection services)
    {
        services
            .AddControllers(o =>
            {
                //o.Filters.Add(typeof(ValidationAttributeHandler));
                o.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
            });
    }

    private static void ConfigureSwagger(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "FastFood Products",
                        Version = "v1"
                    });

                var filePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                setup.IncludeXmlComments(filePath);
            });
    }

    private static void ConfigureVersion(this IServiceCollection services)
    {
        services    
            .AddApiVersioning(options =>
             {
                 options.ReportApiVersions = true;
                 options.AssumeDefaultVersionWhenUnspecified = true;
                 options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
             });
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                                                     b => b.MigrationsAssembly("fastfood-products")).LogTo(s => Console.WriteLine(s)));
    }
}
