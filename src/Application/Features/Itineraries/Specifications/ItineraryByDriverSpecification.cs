namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;
#nullable disable warnings
public class ItineraryByDriverSpecification : Specification<Itinerary>
{
    public ItineraryByDriverSpecification(string driverId)
    {
       Query.Where(q => q.DriverId == driverId, driverId!=string.Empty);

    }
}