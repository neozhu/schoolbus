// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Queries.GetById;

public class GetItineraryByDriverIdQuery : ICacheableRequest<List<ItineraryDto>>
{
   public required string DriverId { get; set; }
   public required string TenantId { get; set; }
   public string CacheKey => ItineraryCacheKey.GetByPilotCacheKey($"{DriverId},{TenantId}");
   public MemoryCacheEntryOptions? Options => ItineraryCacheKey.MemoryCacheEntryOptions;
}

public class GetItineraryByDriverIdQueryHandler :
     IRequestHandler<GetItineraryByDriverIdQuery, List<ItineraryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetItineraryByDriverIdQueryHandler> _localizer;

    public GetItineraryByDriverIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetItineraryByDriverIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<List<ItineraryDto>> Handle(GetItineraryByDriverIdQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Itineraries.ApplySpecification(new ItineraryByDriverSpecification(request.TenantId))
                     .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken) ?? throw new NotFoundException($"Itinerary with id: [{request.DriverId}] not found.");;
        return data;
    }
}
