// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Queries.GetAll;

public class GetAllItinerariesQuery : ICacheableRequest<IEnumerable<ItineraryDto>>
{
   public string CacheKey => ItineraryCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => ItineraryCacheKey.MemoryCacheEntryOptions;
}

public class GetAllItinerariesQueryHandler :
     IRequestHandler<GetAllItinerariesQuery, IEnumerable<ItineraryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllItinerariesQueryHandler> _localizer;

    public GetAllItinerariesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllItinerariesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<ItineraryDto>> Handle(GetAllItinerariesQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Itineraries
                     .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


