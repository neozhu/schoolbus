﻿namespace CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;
#nullable disable warnings
public class TripReportAdvancedSpecification : Specification<TripReport>
{
    public TripReportAdvancedSpecification(TripReportAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);

       Query.Where(q => q.TenantId==filter.CurrentUser.TenantId, !filter.CurrentUser.IsSuperAdmin)
            .Where(q => q.Pilot.Phone == filter.CurrentUser.PhoneNumber, filter.CurrentUser.IsPilots)
             .Where(q => q.Comments!.Contains(filter.Keyword) || q.PlatNumber!.Contains(filter.Keyword), !string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == TripReportListView.My && filter.CurrentUser is not null)
             .Where(q => q.Created >= start && q.Created <= end, filter.ListView == TripReportListView.CreatedToday)
             .Where(q => q.Created >= last30day, filter.ListView == TripReportListView.Created30Days);
       
    }
}
