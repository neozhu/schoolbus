using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;
#nullable disable warnings
public class TripLogByIdSpecification : Specification<TripLog>
{
    public TripLogByIdSpecification(int tripId)
    {
        Query.Where(q => q.TripId == tripId && q.GetOffDateTime2==null);
        Query.OrderByDescending(x => x.GetOnDateTime);
    }
}
public class TripAccidentByIdSpecification : Specification<TripAccident>
{
    public TripAccidentByIdSpecification(int tripId)
    {
        Query.Where(q => q.TripId == tripId);
        Query.Include(x => x.TripReport);
        Query.OrderByDescending(x => x.ReportDateTime);
    }
}