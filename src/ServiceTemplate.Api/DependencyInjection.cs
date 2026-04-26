using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceTemplate.Api.Auth;
using ServiceTemplate.Api.Exceptions;
using ServiceTemplate.Api.Options;

namespace ServiceTemplate.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ApiKeyAuthOptions>()
            .BindConfiguration(ApiKeyAuthOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services
            .AddOptions<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName)
            .ValidateOnStart();

        services
            .AddAuthentication(ApiKeyAuthenticationHandler.SchemeName)
            .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
                ApiKeyAuthenticationHandler.SchemeName,
                _ => { });

        services.AddAuthorization();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ServiceTemplate API",
                Description = "ServiceTemplate API"
            });

            var headerName = configuration.GetValue<string>($"{ApiKeyAuthOptions.SectionName}:{nameof(ApiKeyAuthOptions.HeaderName)}")
                ?? "X-Api-Key";

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = headerName,
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Scheme = ApiKeyAuthenticationHandler.SchemeName,
                Description = "API Key authentication via header."
            };

            options.AddSecurityDefinition(ApiKeyAuthenticationHandler.SchemeName, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
        });

        return services;
    }
}

