using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceTemplate.Api.HealthChecks;

namespace ServiceTemplate.Api.Configuration;

public static class ServiceDefaultsExtensions
{
    public const string CorsPolicyName = "CustomCors";

    public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddCors(options => options.AddPolicy(
            name: CorsPolicyName,
            corsPolicyBuilder =>
            {
                var origins = (builder.Configuration.GetSection("CorsConfiguration:Origins")
                        .Get<string[]>() ?? Array.Empty<string>())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToArray();

                if (builder.Environment.IsDevelopment())
                {
                    corsPolicyBuilder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                }
                else if (origins.Any() && !origins.Any(o => o.Equals("all", StringComparison.OrdinalIgnoreCase)))
                {
                    corsPolicyBuilder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(origins);
                }
            }));

        builder.Services.AddHealthChecks()
            .AddCheck<PostgresReadyHealthCheck>("postgres", tags: ["ready"]);

        return builder;
    }
}

