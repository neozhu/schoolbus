namespace CleanArchitecture.Blazor.Application.Features.Pilots.Specifications;
#nullable disable warnings
public class PilotByIdSpecification : Specification<Pilot>
{
    public PilotByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}