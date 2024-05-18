using fastfood_products.API.HealthCheck;
using fastfood_products.API.Middleware;
using fastfood_products.Application.Shared.Behavior;
using fastfood_products.Infra.IoC;
using fastfood_products.Infra.SqlServer.Context;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using System.Text.Unicode;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;
ILoggingBuilder logging = builder.Logging;

configuration
    .AddUserSecrets<Program>(optional: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
Assembly assembly = Assembly.GetExecutingAssembly();
logging.ClearProviders();
logging.AddConsole();

string? conStr = configuration.GetConnectionString("SqlServerConnection");
if (string.IsNullOrWhiteSpace(conStr))
    throw new InvalidOperationException(
        $"Could not find a connection string named 'SqlServerConnection'.");

services
    .AddHealthChecks()
    .AddCheck<SimpleHealthCheck>("live", failureStatus: HealthStatus.Unhealthy, tags: new[] { "live" });

services
    .AddControllers(o =>
    {
        o.Filters.Add(typeof(ValidationBehavior));
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

        string filePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        setup.IncludeXmlComments(filePath);
    });

services
    .AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    });


services.RegisterServices(configuration);

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseSwagger().UseSwaggerUI();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = _ => _.Tags.Contains("ready"),
    ResponseWriter = HealthCheckResponseWriter.Write
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => _.Tags.Contains("live"),
    ResponseWriter = HealthCheckResponseWriter.Write
});

app.MapDefaultControllerRoute();

app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }