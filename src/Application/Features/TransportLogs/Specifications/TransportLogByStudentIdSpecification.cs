namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;
#nullable disable warnings
public class TransportLogByStudentIdSpecification : Specification<TransportLog>
{
    public TransportLogByStudentIdSpecification(int studentid, int top)
    {
        Query.Where(q => q.StudentId == studentid);
        Query.OrderByDescending(x => x.SwipeDateTime);
        Query.Take(top);
        Query.Include(x => x.Student).Include(x => x.Itinerary);
    }
}