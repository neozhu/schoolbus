namespace CleanArchitecture.Blazor.Application.Features.Students.Specifications;
#nullable disable warnings
public class StudentByIdSpecification : Specification<Student>
{
    public StudentByIdSpecification(int id)
    {
       Query.Where(q => q.Id == id);
    }
}