using Serilog;
using Serilog.Filters;
using ServiceTemplate.Api;
using ServiceTemplate.Api.Options;
using ServiceTemplate.Api.Configuration;
using ServiceTemplate.Api.Observability;
using ServiceTemplate.Infrastructure;
using ServiceTemplate.Application;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddServiceObservability();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.Filter.ByExcluding(Matching.WithProperty<string>("RequestPath", p => p.StartsWith("/health", StringComparison.OrdinalIgnoreCase)));
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApi(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseServiceDefaults();
app.MapServiceDefaults();
await app.ApplyDatabaseMigrationsIfEnabledAsync();

app.Run();

