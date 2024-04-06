namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;
#nullable disable warnings
public class ItineraryByIdSpecification : Specification<Itinerary>
{
    public ItineraryByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}