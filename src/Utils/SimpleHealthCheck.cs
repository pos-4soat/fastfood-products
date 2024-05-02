using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace fastfood_products.Utils
{
    public class SimpleHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception ex)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(exception: ex));
            }
        }
    }
}
