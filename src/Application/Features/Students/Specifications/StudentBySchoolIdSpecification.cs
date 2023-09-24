namespace CleanArchitecture.Blazor.Application.Features.Students.Specifications;
#nullable disable warnings
public class StudentBySchoolIdSpecification : Specification<Student>
{
    public StudentBySchoolIdSpecification(int schoolId,string? name)
    {
        Query.Where(q => q.SchoolId == schoolId)
             .Where(x=>x.UID.StartsWith(name)|| x.LastName.StartsWith(name) || x.FirstName.StartsWith(name), !string.IsNullOrEmpty(name));
        
        Query.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
    }
}