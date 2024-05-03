namespace CleanArchitecture.Blazor.Application.Features.Students.Specifications;
#nullable disable warnings
public class StudentByUIDSpecification : Specification<Student>
{
    public StudentByUIDSpecification(string uid, string tenantId)
    {
        Query.Where(q => q.UID == uid && q.TenantId== tenantId);

    }
}