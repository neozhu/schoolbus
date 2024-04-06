namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;
#nullable disable warnings
public enum ItineraryListView
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

public class ItineraryAdvancedFilter: PaginationFilter
{
    public ItineraryListView ListView { get; set; } = ItineraryListView.All;
    public UserProfile? CurrentUser { get; set; }
}