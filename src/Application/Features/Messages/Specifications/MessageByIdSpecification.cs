namespace CleanArchitecture.Blazor.Application.Features.Messages.Specifications;
#nullable disable warnings
public class MessageByIdSpecification : Specification<Message>
{
    public MessageByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}