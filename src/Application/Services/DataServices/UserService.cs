using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;
using CleanArchitecture.Blazor.Application.Features.Identity.Dto;
using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Blazor.Domain.Identity;

namespace CleanArchitecture.Blazor.Application.Services.DataServices;

public class UserService : IUserService
{
    private readonly IAppCache _cache;
    private readonly UserManager<ApplicationUser> _context;
    private readonly IMapper _mapper;
    public const string CACHEKEY = "ApplicationUserCacheKey";
    public UserService(
        IAppCache cache,
        IServiceScopeFactory scopeFactory,
        IMapper mapper)
    {
        _cache = cache;
        var scope = scopeFactory.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _mapper = mapper;
    }

    public event Action? OnChange;
    public List<ApplicationUserDto> DataSource { get; private set; } = new();

    public async Task InitializeAsync()
    {
        DataSource = await _cache.GetOrAddAsync(CACHEKEY,
            () => _context.Users.OrderBy(x=>x.UserName)
                .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(),TimeSpan.FromSeconds(30)
             );
    }

    public void Initialize()
    {
        DataSource = _cache.GetOrAdd(CACHEKEY,
           () => _context.Users.OrderBy(x => x.UserName)
               .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
               .ToList(), TimeSpan.FromSeconds(30)
            );
    }

    public async Task Refresh()
    {
        _cache.Remove(CACHEKEY);
        DataSource = await _cache.GetOrAddAsync(CACHEKEY,
              () => _context.Users.OrderBy(x => x.UserName)
                  .ProjectTo<ApplicationUserDto>(_mapper.ConfigurationProvider)
                  .ToListAsync(), TimeSpan.FromSeconds(30)
               );
        OnChange?.Invoke();
    }
}