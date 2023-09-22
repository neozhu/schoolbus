// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Pilots.Queries.GetAll;

public class GetAllPilotsQuery : ICacheableRequest<IEnumerable<PilotDto>>
{
   public string CacheKey => PilotCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => PilotCacheKey.MemoryCacheEntryOptions;
}

public class GetAllPilotsQueryHandler :
     IRequestHandler<GetAllPilotsQuery, IEnumerable<PilotDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllPilotsQueryHandler> _localizer;

    public GetAllPilotsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllPilotsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<PilotDto>> Handle(GetAllPilotsQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Pilots
                     .ProjectTo<PilotDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


