namespace CleanArchitecture.Blazor.Application.Features.Buses.Specifications;
#nullable disable warnings
public class BusByIdSpecification : Specification<Bus>
{
    public BusByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}