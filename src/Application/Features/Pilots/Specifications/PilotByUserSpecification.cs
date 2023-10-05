namespace CleanArchitecture.Blazor.Application.Features.Pilots.Specifications;
#nullable disable warnings
public class PilotByUserSpecification : Specification<Pilot>
{
    public PilotByUserSpecification(UserProfile user)
    {
        Query.Where(x => x.LastName != null, user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId, user.IsOrgAdmin);
        Query.Where(q=>q.Phone==user.PhoneNumber,user.IsPilots);
    }
}