// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Queries.GetById;

public class GetItineraryByIdQuery : ICacheableRequest<ItineraryDto>
{
   public required int Id { get; set; }
   public string CacheKey => ItineraryCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => ItineraryCacheKey.MemoryCacheEntryOptions;
}

public class GetItineraryByIdQueryHandler :
     IRequestHandler<GetItineraryByIdQuery, ItineraryDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetItineraryByIdQueryHandler> _localizer;

    public GetItineraryByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetItineraryByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<ItineraryDto> Handle(GetItineraryByIdQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Itineraries.ApplySpecification(new ItineraryByIdSpecification(request.Id))
                     .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Itinerary with id: [{request.Id}] not found.");;
        return data;
    }
}
