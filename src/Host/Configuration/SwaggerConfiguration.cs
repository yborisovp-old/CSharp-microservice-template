namespace ServiceTemplate.Configuration;

/// <summary>
/// Swagger settings configuration
/// </summary>
public class SwaggerConfiguration
{
    /// <summary>
    /// Swagger state flag. When true, Swagger UI will be enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = false;
    
    /// <summary>
    /// Optional endpoint prefix for Swagger UI. If specified, Swagger will be available at /{EndpointPrefix}/swagger
    /// </summary>
    public string EndpointPrefix { get; set; } = string.Empty;
}
