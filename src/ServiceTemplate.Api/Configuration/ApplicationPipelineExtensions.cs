using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using ServiceTemplate.Api.Configuration;

namespace ServiceTemplate.Api.Configuration;

public static class ApplicationPipelineExtensions
{
    public static WebApplication UseServiceDefaults(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandler();

        app.UseCors(ServiceDefaultsExtensions.CorsPolicyName);

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStatusCodePages();

        return app;
    }

    public static WebApplication MapServiceDefaults(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("ready")
        });

        app.MapPrometheusScrapingEndpoint("/metrics");

        app.MapControllers();

        return app;
    }
}

