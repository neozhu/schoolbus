using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;
public interface IPilotService
{
    List<PilotDto> DataSource { get; }

    event Action? OnChange;
    Task<int> GetTotalTrip(string driverId);
    Task<List<ItineraryDto>> GetItineraries(string driverId);
    void Initialize();
    Task InitializeAsync();
    Task Refresh();
}