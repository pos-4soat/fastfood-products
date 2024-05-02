using fastfood_products.Data.Context;
using fastfood_products.Handlers;
using fastfood_products.IoC;
using fastfood_products.Utils;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;
ILoggingBuilder logging = builder.Logging;

configuration
    .AddUserSecrets<Program>(optional: true)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

logging.ConfigureLogging();

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

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = _ => _.Tags.Contains("ready"),
    ResponseWriter = HealthCheckResponseWriter.WriteResponse
});

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = _ => _.Tags.Contains("live"),
    ResponseWriter = HealthCheckResponseWriter.WriteResponse
});

app.MapDefaultControllerRoute();

app.MapControllers();

app.UseHttpsRedirection();

app.Run();