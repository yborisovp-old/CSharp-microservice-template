using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.Api.Options;

public sealed class ApiKeyAuthOptions
{
    public const string SectionName = "ApiKeyAuth";

    [Required]
    [MinLength(1)]
    public string HeaderName { get; init; } = "X-Api-Key";

    [Required]
    [MinLength(1)]
    public string ApiKey { get; init; } = "dev";
}

