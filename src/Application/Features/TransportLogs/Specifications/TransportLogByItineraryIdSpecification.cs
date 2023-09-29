namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;
#nullable disable warnings
public class TransportLogByItineraryIdSpecification : Specification<TransportLog>
{
    public TransportLogByItineraryIdSpecification(int itineraryId, int top)
    {
       Query.Where(q => q.ItineraryId == itineraryId);
       Query.Take(top,top>0);
    }
}
public class TransportLogByItineraryIdOnboardSpecification : Specification<TransportLog>
{
    public TransportLogByItineraryIdOnboardSpecification(int itineraryId, int top=0)
    {
        Query.Where(q => q.ItineraryId == itineraryId && q.Done==false && q.SwipeDateTime.Date==DateTime.Today);
        Query.OrderByDescending(x => x.Id);
        Query.Take(top, top > 0);
        Query.Include(x => x.Student);
    }
}