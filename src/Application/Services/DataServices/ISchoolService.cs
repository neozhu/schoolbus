using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;
public interface ISchoolService
{
    List<SchoolDto> DataSource { get; }

    event Action? OnChange;

    void Initialize();
    Task InitializeAsync();
    Task Refresh();
}