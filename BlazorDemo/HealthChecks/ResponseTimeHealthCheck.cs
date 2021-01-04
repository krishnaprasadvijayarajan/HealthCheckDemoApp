using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorDemo.HealthChecks
{
    public class ResponseTimeHealthCheck : IHealthCheck
    {
        // Core 5 feature - shorthand initialization of Known Class objects
        private Random rnd = new();
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            int responseTimeInMS = rnd.Next(1, 300);
            // Core 5 feature - Logical and Relational switch 
            return responseTimeInMS switch
            {
                <100 => Task.FromResult(HealthCheckResult.Healthy($"The response time looks good ({responseTimeInMS}) ms")),
                >=100 and <200 => Task.FromResult(HealthCheckResult.Degraded($"The response time is a bit slow ({responseTimeInMS}) ms")),
                >=200 => Task.FromResult(HealthCheckResult.Unhealthy($"The response time is unacceptable ({responseTimeInMS}) ms"))
            };
        }
    }
}
