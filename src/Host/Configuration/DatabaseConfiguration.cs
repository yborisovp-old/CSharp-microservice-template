namespace ServiceTemplate.Configuration;

/// <summary>
/// Database configuration class
/// </summary>
public class DatabaseConfiguration
{
    /// <summary>
    /// Database user name
    /// </summary>
    public string DbUserName { get; set; } = string.Empty;
    
    /// <summary>
    /// Database password
    /// </summary>
    public string DbPassword { get; set; } = string.Empty;
    
    /// <summary>
    /// Database host, db and others settings
    /// </summary>
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
