namespace CleanArchitecture.Blazor.Application.Features.Pilots.Specifications;
#nullable disable warnings
public enum PilotListView
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

public class PilotAdvancedFilter: PaginationFilter
{
    public PilotListView ListView { get; set; } = PilotListView.All;
    public UserProfile? CurrentUser { get; set; }
}