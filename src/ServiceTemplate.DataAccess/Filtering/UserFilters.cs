namespace ServiceTemplate.DataAccess.Filtering;

/// <summary>
/// Example filter class demonstrating how to create custom filters.
/// This is a sample implementation - replace with your own filter classes as needed.
/// </summary>
/// <example>
/// <code>
/// // Usage example:
/// var filters = new UserFilters 
/// { 
///     UserName = "john", 
///     PageNumber = 1, 
///     PageSize = 20 
/// };
/// 
/// var result = await repository.GetFilteredAsync(filters, ct);
/// </code>
/// </example>
public class UserFilters : BaseFilters
{
    /// <summary>
    /// Filter by user name (partial match)
    /// </summary>
    public string? UserName { get; set; }
}
