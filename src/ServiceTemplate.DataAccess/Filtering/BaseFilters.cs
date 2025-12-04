using System.ComponentModel.DataAnnotations;

namespace ServiceTemplate.DataAccess.Filtering;

/// <summary>
/// Base filter class for pagination and common filtering operations.
/// Inherit from this class to create your own filter classes.
/// </summary>
/// <example>
/// <code>
/// public class UserFilters : BaseFilters
/// {
///     public string? UserName { get; set; }
///     public string? Email { get; set; }
/// }
/// </code>
/// </example>
public abstract class BaseFilters
{
    /// <summary>
    /// Page number for pagination (1-based)
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
    public int PageNumber { get; set; } = 1;
    
    /// <summary>
    /// Number of items per page
    /// </summary>
    [Range(1, 1000, ErrorMessage = "Page size must be between 1 and 1000")]
    public int PageSize { get; set; } = 50;
    
    /// <summary>
    /// Calculates the number of items to skip for pagination
    /// </summary>
    public int Skip => (PageNumber - 1) * PageSize;
    
    /// <summary>
    /// Calculates the number of items to take for pagination
    /// </summary>
    public int Take => PageSize;
}

