namespace CleanArchitecture.Blazor.Application.Features.Schools.Specifications;
#nullable disable warnings
public enum SchoolListView
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

public class SchoolAdvancedFilter: PaginationFilter
{
    public SchoolListView ListView { get; set; } = SchoolListView.All;
    public UserProfile? CurrentUser { get; set; }
}