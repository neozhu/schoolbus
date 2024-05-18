namespace CleanArchitecture.Blazor.Application.Features.Dashboard.Specifications;
#nullable disable warnings
public class SummaryBusSpecification : Specification<Bus>
{
    public SummaryBusSpecification(UserProfile user)
    {
       Query.Where(q => q.PlatNumber!="", user.IsSuperAdmin);
       Query.Where(q => q.TenantId==user.TenantId && q.PlatNumber != "", user.IsOrgAdmin);
       Query.Where(q => q.TenantId == user.TenantId&& q.PlatNumber != "", !user.IsSuperAdmin&&!user.IsOrgAdmin);

    }
}
public class SummarySchoolSpecification : Specification<School>
{
    public SummarySchoolSpecification(UserProfile user)
    {
        Query.Where(q => q.Name != "", user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId && q.Name != "", !user.IsSuperAdmin);
    }
}
public class SummaryPilotSpecification : Specification<Pilot>
{
    public SummaryPilotSpecification(UserProfile user)
    {
        Query.Where(q => q.TenantId != "", user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId && q.LastName != "", !user.IsSuperAdmin);
    }
}
public class SummaryParentSpecification : Specification<Parent>
{
    public SummaryParentSpecification(UserProfile user)
    {
        Query.Where(q => q.LastName != "", user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId && q.LastName != "", !user.IsSuperAdmin);
    }
}

public class SummaryStudentSpecification : Specification<Student>
{
    public SummaryStudentSpecification(UserProfile user)
    {
        Query.Where(q => q.LastName != "", user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId && q.LastName != "", !user.IsSuperAdmin);
    }
}

public class SummaryItinerarySpecification : Specification<Itinerary>
{
    public SummaryItinerarySpecification(UserProfile user)
    {
        Query.Where(q => q.Name != "", user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId && q.Name != "", !user.IsSuperAdmin);
    }
}
public class SummaryTransportLogSpecification : Specification<TransportLog>
{
    public SummaryTransportLogSpecification(UserProfile user)
    {
        Query.Where(q => q.Id >0, user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId, !user.IsSuperAdmin);
    }
}
public class SummaryTripReportSpecification : Specification<TripReport>
{
    public SummaryTripReportSpecification(UserProfile user)
    {
        Query.Where(q => q.ItineraryId !=null, user.IsSuperAdmin);
        Query.Where(q => q.TenantId == user.TenantId && q.ItineraryId != null, !user.IsSuperAdmin);
    }
}