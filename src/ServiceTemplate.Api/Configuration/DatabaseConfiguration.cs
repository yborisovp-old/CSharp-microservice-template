using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.Configuration;

/// <summary>
/// Database configuration class
/// </summary>
public class DatabaseConfiguration
{
    [Required(ErrorMessage = "Database user name is required")]
    [MinLength(1, ErrorMessage = "Database user name cannot be empty")]
    public string DbUserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Database password is required")]
    [MinLength(1, ErrorMessage = "Database password cannot be empty")]
    public string DbPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Database connection string is required")]
    [MinLength(1, ErrorMessage = "Database connection string cannot be empty")]
    public string DbConnection { get; set; } = string.Empty;

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

