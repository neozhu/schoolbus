namespace CleanArchitecture.Blazor.Application.Features.Students.Specifications;
#nullable disable warnings
public class StudentByUIDSpecification : Specification<Student>
{
    public StudentByUIDSpecification(string uid)
    {
        Query.Where(q => q.UID == uid);
        Query.Include(x => x.School);

    }
}