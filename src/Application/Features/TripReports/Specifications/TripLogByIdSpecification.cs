namespace CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;
#nullable disable warnings
public class TripLogByIdSpecification : Specification<TripLog>
{
    public TripLogByIdSpecification(int tripId)
    {
        Query.Where(q => q.TripId == tripId && q.GetOffDateTime2==null);
        Query.Include(x => x.TripReport)
            .Include(x => x.Student);
        Query.OrderByDescending(x => x.GetOnDateTime);
    }
}