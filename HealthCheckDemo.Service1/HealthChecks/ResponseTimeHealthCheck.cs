using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HealthCheckDemo.Service1.HealthChecks
{
    public class ResponseTimeHealthCheck : IHealthCheck
    {
        public Random random = new Random();

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            int responseTimeInMs = random.Next(1, 300);

            if (responseTimeInMs < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy($"Kaymak gibi çalışıyor ({responseTimeInMs}). "));
            }
            else if(responseTimeInMs < 200)
            {
                return Task.FromResult(HealthCheckResult.Degraded($"Azcık yavaş çalışıyor ({responseTimeInMs}). "));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy($"Aloooooo Çalışmıyoooooooo ({responseTimeInMs}). "));
            }
        }


    }
}
