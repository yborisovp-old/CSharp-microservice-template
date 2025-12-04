namespace ServiceTemplate.Configuration;

/// <summary>
/// Base configuration
/// </summary>
public class BaseConfiguration
{
    /// <summary>
    /// Database connection config
    /// </summary>
    public DatabaseConfiguration DatabaseConfig { get; set; } = new();
    /// <summary>
    /// Swagger config
    /// </summary>
    public SwaggerConfiguration SwaggerConfig { get; set; } = new();
}
