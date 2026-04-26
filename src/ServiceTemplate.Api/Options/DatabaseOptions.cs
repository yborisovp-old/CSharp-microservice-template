namespace ServiceTemplate.Api.Options;

public sealed class DatabaseOptions
{
    public const string SectionName = "Database";

    public string? ConnectionString { get; init; }

    public bool ApplyMigrationsOnStartup { get; init; } = false;
}

