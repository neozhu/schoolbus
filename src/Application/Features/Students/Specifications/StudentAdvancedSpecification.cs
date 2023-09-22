namespace CleanArchitecture.Blazor.Application.Features.Students.Specifications;
#nullable disable warnings
public class StudentAdvancedSpecification : Specification<Student>
{
    public StudentAdvancedSpecification(StudentAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);

       Query.Where(q => q.UID != null)
             .Where(q => q.UID!.Contains(filter.Keyword) || q.LastName!.Contains(filter.Keyword)|| q.FirstName!.Contains(filter.Keyword)|| q.Phone.Contains(filter.Keyword) || q.Description!.Contains(filter.Keyword), !string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == StudentListView.My && filter.CurrentUser is not null)
             .Where(q => q.Created >= start && q.Created <= end, filter.ListView == StudentListView.CreatedToday)
             .Where(q => q.Created >= last30day, filter.ListView == StudentListView.Created30Days);
       
    }
}
