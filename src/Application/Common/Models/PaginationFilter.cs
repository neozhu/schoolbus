namespace CleanArchitecture.Blazor.Application.Common.Models;

public partial class PaginationFilter : SortableFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 15;
    public override string ToString() => $"PageNumber:{PageNumber},PageSize:{PageSize},OrderBy:{OrderBy},SortDirection:{SortDirection},Keyword:{Keyword}";
}
public partial class SortableFilter : BaseFilter
{
    public string OrderBy { get; set; } = "Id";
    public string SortDirection { get; set; } = "Descending";
    public override string ToString() => $"OrderBy:{OrderBy},SortDirection:{SortDirection},Keyword:{Keyword}";
}
public class BaseFilter
{
    public string? Keyword { get; set; }
}

