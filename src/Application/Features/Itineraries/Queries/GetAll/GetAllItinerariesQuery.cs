// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Queries.GetAll;

public class GetAllItinerariesQuery : ICacheableRequest<IEnumerable<ItineraryDto>>
{
   public string CacheKey => ItineraryCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => ItineraryCacheKey.MemoryCacheEntryOptions;
}
public class GetItinerariesByPilotQuery : ICacheableRequest<IEnumerable<ItineraryDto>>
{
    public required UserProfile UserProfile { get; set; }
    public string CacheKey => ItineraryCacheKey.GetByPilotCacheKey($"{UserProfile.UserId}");
    public MemoryCacheEntryOptions? Options => ItineraryCacheKey.MemoryCacheEntryOptions;
}

public class GetAllItinerariesQueryHandler :
    IRequestHandler<GetItinerariesByPilotQuery, IEnumerable<ItineraryDto>>,
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
    public async Task<IEnumerable<ItineraryDto>> Handle(GetItinerariesByPilotQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Itineraries.ApplySpecification(new ItineraryByPilotSpecification(request.UserProfile))
                     .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


