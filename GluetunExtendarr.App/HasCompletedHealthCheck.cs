using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GluetunExtendarr.App;

public class HasCompletedHealthCheck(ICompletable completable) : IHealthCheck
{
    private readonly HealthCheckResult healthy = HealthCheckResult.Healthy("Config file has been created");
    private readonly HealthCheckResult unhealthy = HealthCheckResult.Unhealthy($"{nameof(ExtendarrRunner)} has not completed yet");

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken()) => Task.FromResult(completable.HasCompleted ? healthy : unhealthy);
}