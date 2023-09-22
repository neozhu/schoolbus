namespace CleanArchitecture.Blazor.Application.Features.Students.Specifications;
#nullable disable warnings
public enum StudentListView
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

public class StudentAdvancedFilter: PaginationFilter
{
    public StudentListView ListView { get; set; } = StudentListView.All;
    public UserProfile? CurrentUser { get; set; }
}