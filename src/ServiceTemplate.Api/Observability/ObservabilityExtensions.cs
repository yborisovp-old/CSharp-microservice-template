using System.Diagnostics;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ServiceTemplate.Api.Observability;

public static class ObservabilityExtensions
{
    public static readonly ActivitySource ActivitySource = new("ServiceTemplate");

    public static WebApplicationBuilder AddServiceObservability(this WebApplicationBuilder builder)
    {
        var serviceName = builder.Configuration["Service:Name"]
            ?? builder.Environment.ApplicationName;
        var serviceVersion = typeof(Program).Assembly.GetName().Version?.ToString()
            ?? "unknown";

        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
            logging.ParseStateValues = true;
            logging.SetResourceBuilder(ResourceBuilder.CreateDefault()
                .AddService(serviceName: serviceName, serviceVersion: serviceVersion));
            logging.AddOtlpExporter();
        });

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(serviceName: serviceName, serviceVersion: serviceVersion);
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(ActivitySource.Name)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddOtlpExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddMeter("ServiceTemplate")
                    .AddPrometheusExporter();
            });

        return builder;
    }
}

