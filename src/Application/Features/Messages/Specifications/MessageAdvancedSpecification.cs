namespace CleanArchitecture.Blazor.Application.Features.Messages.Specifications;
#nullable disable warnings
public class MessageAdvancedSpecification : Specification<Message>
{
    public MessageAdvancedSpecification(MessageAdvancedFilter filter)
    {
        var today = DateTime.Now.ToUniversalTime().Date;
        var start = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);
        var end = Convert.ToDateTime(today.ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 23:59:59",
            CultureInfo.CurrentCulture);
        var last30day = Convert.ToDateTime(
            today.AddDays(-30).ToString("yyyy-MM-dd", CultureInfo.CurrentCulture) + " 00:00:00",
            CultureInfo.CurrentCulture);

        Query.Where(q => q.From != null)
             .Where(q => q.TenantId == filter.CurrentUser.TenantId, !filter.CurrentUser.IsSuperAdmin)
              .Where(q => q.Content!.Contains(filter.Keyword) || q.To!.Contains(filter.Keyword) || q.From!.Contains(filter.Keyword), !string.IsNullOrEmpty(filter.Keyword))
              .Where(q => q.CreatedBy == filter.CurrentUser.UserId, filter.ListView == MessageListView.My && filter.CurrentUser is not null)
              .Where(q => q.Created >= start && q.Created <= end, filter.ListView == MessageListView.CreatedToday)
              .Where(q => q.Created >= last30day, filter.ListView == MessageListView.Created30Days);

    }
}
