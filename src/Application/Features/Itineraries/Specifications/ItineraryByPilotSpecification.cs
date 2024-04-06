namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;
#nullable disable warnings
public class ItineraryByPilotSpecification : Specification<Itinerary>
{
    public ItineraryByPilotSpecification(UserProfile user)
    {
       Query.Where(q => q.BusId>0, user.IsSuperAdmin);
       Query.Where(q => q.TenantId==user.TenantId, user.IsOrgAdmin);
       Query.Where(q => q.Pilot.Phone == user.PhoneNumber, user.IsPilots);

    }
}