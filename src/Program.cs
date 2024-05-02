using fastfood_products.Data.Context;
using fastfood_products.Handlers;
using fastfood_products.IoC;
using fastfood_products.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.SwaggerGen;
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

logging.ClearProviders();
logging.AddConsole();

//services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
//services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
//services.AddFluentValidationRulesToSwagger();

services
    .AddControllers(o =>
    {
        //o.Filters.Add(typeof(ValidationAttributeHandler));
        o.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

services
    .Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

var conStr = builder.Configuration.GetConnectionString("SqlServerConnection");
if (string.IsNullOrWhiteSpace(conStr))
    throw new InvalidOperationException(
        $"Could not find a connection string named 'SqlServerConnection'.");

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(setup =>
    {
        setup.SwaggerDoc("v1",
            new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "FastFood Products",
                Version = "v1",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                {
                    //TODO - Add e-mail and name
                    Email = "",
                    Name = "",
                }
            });

        var filePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        setup.IncludeXmlComments(filePath);
    })
    .AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    })
    .AddHealthChecks()
    .AddCheck<SimpleHealthCheck>(
        "HealthCheck",
        tags: new[] { "HealthCheck" })
    .AddSqlServer(conStr);

services.RegisterServices(configuration);

WebApplication app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseSwagger().UseSwaggerUI();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    app.MapHealthChecks("health/readiness", new HealthCheckOptions
    {
        Predicate = healthCheck => healthCheck.Tags.Contains("HealthCheck"),
    });

    app.MapHealthChecks("health/liveness", new HealthCheckOptions
    {
        Predicate = healthCheck => healthCheck.Tags.Contains("HealthCheck"),
    });

    endpoints.MapControllers();
});
app.UseHttpsRedirection();

app.MapControllerRoute(name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();