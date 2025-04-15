namespace Nexus.Sdk.Token.API.Models.Response;
public class PagedResult<T>
{
    public int Page { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public IDictionary<string, string> FilteringParameters { get; set; }
    public IEnumerable<T> Records { get; set; }
}

public class TotalsResult<T>
{
    public IDictionary<string, string> FilteringParameters { get; set; }
    public IEnumerable<T> Totals { get; set; }
}