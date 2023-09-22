namespace CleanArchitecture.Blazor.Application.Features.Parents.Specifications;
#nullable disable warnings
public class ParentByIdSpecification : Specification<Parent>
{
    public ParentByIdSpecification(int id)
    {
        Query.Where(q => q.Id == id);
    }
}