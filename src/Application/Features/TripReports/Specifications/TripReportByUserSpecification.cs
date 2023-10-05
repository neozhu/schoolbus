namespace CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;
#nullable disable warnings
public class TripReportByUserSpecification : Specification<TripReport>
{
    public TripReportByUserSpecification(UserProfile user, TripStatus? status)
    {
        Query.Where(q => q.Id > 0, user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId, user.IsOrgAdmin);
        Query.Where(q => q.Pilot.Phone == user.PhoneNumber, user.IsPilots);
        Query.Where(q => q.Status == status, status!=null);
        Query.OrderByDescending(x => x.Created);
    }
}