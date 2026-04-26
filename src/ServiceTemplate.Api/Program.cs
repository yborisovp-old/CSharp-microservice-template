using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Filters;
using ServiceTemplate.Api;
using ServiceTemplate.Api.HealthChecks;
using ServiceTemplate.Api.Options;
using ServiceTemplate.Api.Observability;
using ServiceTemplate.Infrastructure;
using ServiceTemplate.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceObservability();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.Filter.ByExcluding(Matching.WithProperty<string>("RequestPath", p => p.StartsWith("/health", StringComparison.OrdinalIgnoreCase)));
});

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

builder.Services.AddHealthChecks()
    .AddCheck<PostgresReadyHealthCheck>("postgres", tags: ["ready"]);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApi(builder.Configuration);

const string customPolicyName = "CustomCors";
var origins = (builder.Configuration.GetSection($"CorsConfiguration:Origins")
        .Get<string[]>() ?? Array.Empty<string>())
    .Where(s => !string.IsNullOrEmpty(s))
    .ToArray();

builder.Services.AddCors(options => options.AddPolicy(name: customPolicyName,
    corsPolicyBuilder =>
    {
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
                .WithOrigins(origins.ToArray());
        }
    }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health/live", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => false
});

app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("ready")
});

app.MapPrometheusScrapingEndpoint("/metrics");

if (app.Configuration.GetValue<bool>($"{DatabaseOptions.SectionName}:{nameof(DatabaseOptions.ApplyMigrationsOnStartup)}"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages();

app.MapControllers();

app.Run();

