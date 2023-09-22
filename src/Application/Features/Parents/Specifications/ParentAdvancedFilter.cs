namespace CleanArchitecture.Blazor.Application.Features.Parents.Specifications;
#nullable disable warnings
public enum ParentListView
{
    [Description("All")]
    All,
    [Description("My")]
    My,
    [Description("Created Toady")]
    CreatedToday,
    [Description("Created within the last 30 days")]
    Created30Days
}

public class ParentAdvancedFilter: PaginationFilter
{
    public ParentListView ListView { get; set; } = ParentListView.All;
    public UserProfile? CurrentUser { get; set; }
}