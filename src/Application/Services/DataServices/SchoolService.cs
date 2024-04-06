using CleanArchitecture.Blazor.Application.Features.KeyValues.Caching;
using CleanArchitecture.Blazor.Application.Features.KeyValues.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.Caching;
using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;

public class SchoolService : ISchoolService
{
    private readonly IAppCache _cache;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public SchoolService(
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
    public List<SchoolDto> DataSource { get; private set; } = new();

    public async Task InitializeAsync()
    {
        DataSource = await _cache.GetOrAddAsync(SchoolCacheKey.GetAllCacheKey,
            () => _context.Schools.OrderBy(x => x.TenantId).ThenBy(x => x.Name)
                .ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),
            SchoolCacheKey.MemoryCacheEntryOptions);
    }

    public void Initialize()
    {
        DataSource = _cache.GetOrAdd(SchoolCacheKey.GetAllCacheKey,
            () => _context.Schools.OrderBy(x => x.TenantId).ThenBy(x => x.Name)
                .ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                .ToList(),
            SchoolCacheKey.MemoryCacheEntryOptions);
    }

    public async Task Refresh()
    {
        _cache.Remove(SchoolCacheKey.GetAllCacheKey);
        DataSource = await _cache.GetOrAddAsync(SchoolCacheKey.GetAllCacheKey,
              () => _context.Schools.OrderBy(x => x.TenantId).ThenBy(x => x.Name)
                  .ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                  .ToListAsync(),
              SchoolCacheKey.MemoryCacheEntryOptions);
        OnChange?.Invoke();
    }
}