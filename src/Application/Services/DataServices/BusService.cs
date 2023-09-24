using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;

public class BusService : IBusService
{
    private readonly IAppCache _cache;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public BusService(
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
    public List<BusDto> DataSource { get; private set; } = new();

    public async Task InitializeAsync()
    {
        DataSource = await _cache.GetOrAddAsync(BusCacheKey.GetAllCacheKey,
            () => _context.Buses.OrderBy(x => x.TenantId).ThenBy(x => x.PlatNumber)
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),
            BusCacheKey.MemoryCacheEntryOptions);
    }

    public void Initialize()
    {
        DataSource = _cache.GetOrAdd(BusCacheKey.GetAllCacheKey,
            () => _context.Buses.OrderBy(x => x.TenantId).ThenBy(x => x.PlatNumber)
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .ToList(),
            BusCacheKey.MemoryCacheEntryOptions);
    }

    public async Task Refresh()
    {
        _cache.Remove(BusCacheKey.GetAllCacheKey);
        DataSource = await _cache.GetOrAddAsync(BusCacheKey.GetAllCacheKey,
            () => _context.Buses.OrderBy(x => x.TenantId).ThenBy(x => x.PlatNumber)
                .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),
            BusCacheKey.MemoryCacheEntryOptions
        );
        OnChange?.Invoke();
    }
}