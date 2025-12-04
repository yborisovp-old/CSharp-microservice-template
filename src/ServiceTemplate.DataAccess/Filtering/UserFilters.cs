namespace ServiceTemplate.DataAccess.Filtering;

public class UserFilters
{
    public string? UserName { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}