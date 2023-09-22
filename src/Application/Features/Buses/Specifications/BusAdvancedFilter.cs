namespace CleanArchitecture.Blazor.Application.Features.Buses.Specifications;
#nullable disable warnings
public enum BusListView
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

public class BusAdvancedFilter: PaginationFilter
{
    public BusListView ListView { get; set; } = BusListView.All;
    public UserProfile? CurrentUser { get; set; }
}