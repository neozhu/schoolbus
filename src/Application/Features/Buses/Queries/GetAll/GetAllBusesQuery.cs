// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Buses.Queries.GetAll;

public class GetAllBusesQuery : ICacheableRequest<IEnumerable<BusDto>>
{
   public string CacheKey => BusCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => BusCacheKey.MemoryCacheEntryOptions;
}

public class GetAllBusesQueryHandler :
     IRequestHandler<GetAllBusesQuery, IEnumerable<BusDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllBusesQueryHandler> _localizer;

    public GetAllBusesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllBusesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<BusDto>> Handle(GetAllBusesQuery request, CancellationToken cancellationToken)
    {
  
        var data = await _context.Buses
                     .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


