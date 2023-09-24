using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;
public interface IBusService
{
    List<BusDto> DataSource { get; }

    event Action? OnChange;

    void Initialize();
    Task InitializeAsync();
    Task Refresh();
}