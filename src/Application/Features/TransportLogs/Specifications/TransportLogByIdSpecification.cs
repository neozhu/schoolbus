namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;
#nullable disable warnings
public class TransportLogByIdSpecification : Specification<TransportLog>
{
    public TransportLogByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}