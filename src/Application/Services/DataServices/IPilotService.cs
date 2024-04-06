using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;
public interface IPilotService
{
    List<PilotDto> DataSource { get; }

    event Action? OnChange;

    void Initialize();
    Task InitializeAsync();
    Task Refresh();
}