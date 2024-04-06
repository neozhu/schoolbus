namespace CleanArchitecture.Blazor.Application.Features.Schools.Specifications;
#nullable disable warnings
public class SchoolByIdSpecification : Specification<School>
{
    public SchoolByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}