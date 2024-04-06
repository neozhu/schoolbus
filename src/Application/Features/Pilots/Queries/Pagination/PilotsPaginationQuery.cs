// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.Caching;
using CleanArchitecture.Blazor.Application.Features.Pilots.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Pilots.Queries.Pagination;

public class PilotsWithPaginationQuery : PilotAdvancedFilter, ICacheableRequest<PaginatedData<PilotDto>>
{
    public override string ToString()
    {
        return $"{CurrentUser.UserId},Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => PilotCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => PilotCacheKey.MemoryCacheEntryOptions;
    public PilotAdvancedSpecification Specification => new PilotAdvancedSpecification(this);
}
    
public class PilotsWithPaginationQueryHandler :
         IRequestHandler<PilotsWithPaginationQuery, PaginatedData<PilotDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<PilotsWithPaginationQueryHandler> _localizer;

        public PilotsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<PilotsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<PilotDto>> Handle(PilotsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Pilots.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Pilot, PilotDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}