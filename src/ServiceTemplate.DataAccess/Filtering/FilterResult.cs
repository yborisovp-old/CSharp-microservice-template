namespace ServiceTemplate.DataAccess.Filtering;

public class FilterResult<TResult>
{
    public required IEnumerable<TResult> Results { get; set; }
    public required long Count { get; set; }
}