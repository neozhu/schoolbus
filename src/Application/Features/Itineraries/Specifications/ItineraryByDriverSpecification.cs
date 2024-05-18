namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;
#nullable disable warnings
public class ItineraryByDriverSpecification : Specification<Itinerary>
{
    public ItineraryByDriverSpecification(string tenantId)
    {
       Query.Where(q => q.TenantId == tenantId, !string.IsNullOrEmpty(tenantId));

    }
}