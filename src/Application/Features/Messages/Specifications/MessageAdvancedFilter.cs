namespace CleanArchitecture.Blazor.Application.Features.Messages.Specifications;
#nullable disable warnings
public enum MessageListView
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

public class MessageAdvancedFilter: PaginationFilter
{
    public MessageListView ListView { get; set; } = MessageListView.All;
    public UserProfile? CurrentUser { get; set; }
}