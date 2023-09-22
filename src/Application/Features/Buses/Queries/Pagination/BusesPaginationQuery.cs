// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;
using CleanArchitecture.Blazor.Application.Features.Buses.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Buses.Queries.Pagination;

public class BusesWithPaginationQuery : BusAdvancedFilter, ICacheableRequest<PaginatedData<BusDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => BusCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => BusCacheKey.MemoryCacheEntryOptions;
    public BusAdvancedSpecification Specification => new BusAdvancedSpecification(this);
}
    
public class BusesWithPaginationQueryHandler :
         IRequestHandler<BusesWithPaginationQuery, PaginatedData<BusDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<BusesWithPaginationQueryHandler> _localizer;

        public BusesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<BusesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<BusDto>> Handle(BusesWithPaginationQuery request, CancellationToken cancellationToken)
        {

           var data = await _context.Buses.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Bus, BusDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}