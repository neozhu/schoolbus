﻿namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;
#nullable disable warnings
public class TransportLogAdvancedSpecification : Specification<TransportLog>
{
    public TransportLogAdvancedSpecification(TransportLogAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);

       Query.Where(q => q.ItineraryId != null)
             .Where(q => q.Location!.Contains(filter.Keyword) || q.Comments!.Contains(filter.Keyword), !string.IsNullOrEmpty(filter.Keyword))
             .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == TransportLogListView.My && filter.CurrentUser is not null)
             .Where(q => q.Created >= start && q.Created <= end, filter.ListView == TransportLogListView.CreatedToday)
             .Where(q => q.Created >= last30day, filter.ListView == TransportLogListView.Created30Days);
       
    }
}
