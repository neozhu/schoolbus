// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;
using CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Queries.Pagination;

public class TripReportsWithPaginationQuery : TripReportAdvancedFilter, ICacheableRequest<PaginatedData<TripReportToPlainDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => TripReportCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => TripReportCacheKey.MemoryCacheEntryOptions;
    public TripReportAdvancedSpecification Specification => new TripReportAdvancedSpecification(this);
}
    
public class TripReportsWithPaginationQueryHandler :
         IRequestHandler<TripReportsWithPaginationQuery, PaginatedData<TripReportToPlainDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<TripReportsWithPaginationQueryHandler> _localizer;

        public TripReportsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<TripReportsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<TripReportToPlainDto>> Handle(TripReportsWithPaginationQuery request, CancellationToken cancellationToken)
        {
        
           var data = await _context.TripReports.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<TripReport, TripReportToPlainDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}