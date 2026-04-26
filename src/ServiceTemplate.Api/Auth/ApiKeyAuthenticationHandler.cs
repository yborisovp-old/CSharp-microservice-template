using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using ServiceTemplate.Api.Options;

namespace ServiceTemplate.Api.Auth;

public sealed class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "ApiKey";

    private readonly IOptions<ApiKeyAuthOptions> _options;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> schemeOptions,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IOptions<ApiKeyAuthOptions> options)
        : base(schemeOptions, logger, encoder)
    {
        _options = options;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headerName = _options.Value.HeaderName;

        if (!Request.Headers.TryGetValue(headerName, out var providedKeyValues))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var providedKey = providedKeyValues.ToString();
        if (!string.Equals(providedKey, _options.Value.ApiKey, StringComparison.Ordinal))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "api-key"),
            new Claim(ClaimTypes.Name, "api-key"),
        };
        var identity = new ClaimsIdentity(claims, SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

