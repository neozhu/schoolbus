using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.Caching;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;
using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Common.Mappings;
using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;
using System.Threading;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;

public class PilotService : IPilotService
{
    private readonly IAppCache _cache;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PilotService(
        IAppCache cache,
        IServiceScopeFactory scopeFactory,
        IMapper mapper)
    {
        _cache = cache;
        var scope = scopeFactory.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        _mapper = mapper;
    }

    public event Action? OnChange;
    public List<PilotDto> DataSource { get; private set; } = new();

    public async Task InitializeAsync()
    {
        DataSource = await _cache.GetOrAddAsync(PilotCacheKey.GetAllCacheKey,
            () => _context.Pilots.OrderBy(x => x.TenantId).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                .ProjectTo<PilotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),
            PilotCacheKey.MemoryCacheEntryOptions);
    }

    public void Initialize()
    {
        DataSource = _cache.GetOrAdd(PilotCacheKey.GetAllCacheKey,
            () => _context.Pilots.OrderBy(x => x.TenantId).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                .ProjectTo<PilotDto>(_mapper.ConfigurationProvider)
                .ToList(),
            PilotCacheKey.MemoryCacheEntryOptions);
    }

    public async Task Refresh()
    {
        _cache.Remove(PilotCacheKey.GetAllCacheKey);
        DataSource = await _cache.GetOrAddAsync(PilotCacheKey.GetAllCacheKey,
            () => _context.Pilots.OrderBy(x => x.TenantId).ThenBy(x => x.LastName).ThenBy(x => x.FirstName)
                .ProjectTo<PilotDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),
            PilotCacheKey.MemoryCacheEntryOptions
        );
        OnChange?.Invoke();
    }

    public async Task<int> GetTotalTrip(string driverId)
    {
        var result = await _cache.GetOrAddAsync(TripReportCacheKey.GetAllCacheKey,
            () => _context.TripReports.CountAsync(x=>x.DriverId==driverId)
                ,
            TripReportCacheKey.MemoryCacheEntryOptions);
        return result;
    }


    public Task<List<ItineraryDto>> GetItineraries(string driverId)
    {
        var result = _cache.GetOrAddAsync(ItineraryCacheKey.GetByPilotCacheKey(driverId),
            () => _context.Itineraries.Where(x => x.DriverId == driverId).ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                .ToListAsync()
                ,
            ItineraryCacheKey.MemoryCacheEntryOptions);
        return result;
    }

   
}