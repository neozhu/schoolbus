namespace CleanArchitecture.Blazor.Application.Features.Buses.Specifications;
#nullable disable warnings
public class BusAdvancedSpecification : Specification<Bus>
{
    public BusAdvancedSpecification(BusAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);

       Query.Where(q => q.PlatNumber != null)
            .Where(x => x.TenantId == filter.CurrentUser.TenantId, !filter.CurrentUser.IsSuperAdmin)
             .Where(q => q.PlatNumber!.Contains(filter.Keyword) || q.Description!.Contains(filter.Keyword), !string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == BusListView.My && filter.CurrentUser is not null)
             .Where(q => q.Created >= start && q.Created <= end, filter.ListView == BusListView.CreatedToday)
             .Where(q => q.Created >= last30day, filter.ListView == BusListView.Created30Days);
       
    }
}
