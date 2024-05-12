﻿using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace fastfood_products.API.HealthCheck;

public class SimpleHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}
