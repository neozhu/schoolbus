namespace CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;
#nullable disable warnings
public enum TripReportListView
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

public class TripReportAdvancedFilter: PaginationFilter
{
    public TripReportListView ListView { get; set; } = TripReportListView.All;
    public UserProfile? CurrentUser { get; set; }
}