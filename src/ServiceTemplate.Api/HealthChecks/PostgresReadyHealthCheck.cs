using Microsoft.Extensions.Diagnostics.HealthChecks;
using ServiceTemplate.Infrastructure.Persistence;

namespace ServiceTemplate.Api.HealthChecks;

public sealed class PostgresReadyHealthCheck : IHealthCheck
{
    private readonly AppDbContext _db;

    public PostgresReadyHealthCheck(AppDbContext db)
    {
        _db = db;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var canConnect = await _db.Database.CanConnectAsync(cancellationToken);
            return canConnect
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy("Cannot connect to PostgreSQL.");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Cannot connect to PostgreSQL.", ex);
        }
    }
}

