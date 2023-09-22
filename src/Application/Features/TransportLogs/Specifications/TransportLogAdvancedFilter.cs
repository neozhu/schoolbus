namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;
#nullable disable warnings
public enum TransportLogListView
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

public class TransportLogAdvancedFilter: PaginationFilter
{
    public TransportLogListView ListView { get; set; } = TransportLogListView.All;
    public UserProfile? CurrentUser { get; set; }
}