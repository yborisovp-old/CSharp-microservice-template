using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.Configuration;

/// <summary>
/// Database configuration class
/// </summary>
public class DatabaseConfiguration
{
    /// <summary>
    /// Database user name
    /// </summary>
    [Required(ErrorMessage = "Database user name is required")]
    [MinLength(1, ErrorMessage = "Database user name cannot be empty")]
    public string DbUserName { get; set; } = string.Empty;
    
    /// <summary>
    /// Database password
    /// </summary>
    [Required(ErrorMessage = "Database password is required")]
    [MinLength(1, ErrorMessage = "Database password cannot be empty")]
    public string DbPassword { get; set; } = string.Empty;
    
    /// <summary>
    /// Database host, db and others settings
    /// </summary>
    [Required(ErrorMessage = "Database connection string is required")]
    [MinLength(1, ErrorMessage = "Database connection string cannot be empty")]
    public string DbConnection { get; set; } = string.Empty;

    /// <summary>
    /// Full connection string builder
    /// </summary>
    public string FullConnectionString
        => MergeWithDelimiter(MergeWithDelimiter(DbConnection, $"Username={DbUserName}", ';'),
            $"Password={DbPassword}", ';');

    private string MergeWithDelimiter(string one, string another, char delimiter)
    {
        if (string.IsNullOrEmpty(one))
        {
            return another;
        }
        if (string.IsNullOrEmpty(another))
        {
            return one;
        }
        return $"{one.TrimEnd(delimiter)}{delimiter}{another.TrimStart(delimiter)}";
    }
}
