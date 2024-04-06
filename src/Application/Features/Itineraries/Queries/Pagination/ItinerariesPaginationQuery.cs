// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Queries.Pagination;

public class ItinerariesWithPaginationQuery : ItineraryAdvancedFilter, ICacheableRequest<PaginatedData<ItineraryDto>>
{
    public override string ToString()
    {
        return $"{CurrentUser.UserId},Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => ItineraryCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => ItineraryCacheKey.MemoryCacheEntryOptions;
    public ItineraryAdvancedSpecification Specification => new ItineraryAdvancedSpecification(this);
}
    
public class ItinerariesWithPaginationQueryHandler :
         IRequestHandler<ItinerariesWithPaginationQuery, PaginatedData<ItineraryDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ItinerariesWithPaginationQueryHandler> _localizer;

        public ItinerariesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<ItinerariesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<ItineraryDto>> Handle(ItinerariesWithPaginationQuery request, CancellationToken cancellationToken)
        {
     
           var data = await _context.Itineraries.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Itinerary, ItineraryDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}