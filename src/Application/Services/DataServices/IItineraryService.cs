using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;
public interface IItineraryService
{
    List<ItineraryDto> DataSource { get; }

    event Action? OnChange;

    void Initialize();
    Task InitializeAsync();
    Task Refresh();
}