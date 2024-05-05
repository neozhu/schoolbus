namespace CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;
#nullable disable warnings
public class TripReportByIdSpecification : Specification<TripReport>
{
    public TripReportByIdSpecification(int id)
    {
        Query.Where(q => q.Id == id);
    }
}