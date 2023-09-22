// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Queries.Pagination;

public class TransportLogsWithPaginationQuery : TransportLogAdvancedFilter, ICacheableRequest<PaginatedData<TransportLogDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => TransportLogCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => TransportLogCacheKey.MemoryCacheEntryOptions;
    public TransportLogAdvancedSpecification Specification => new TransportLogAdvancedSpecification(this);
}
    
public class TransportLogsWithPaginationQueryHandler :
         IRequestHandler<TransportLogsWithPaginationQuery, PaginatedData<TransportLogDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<TransportLogsWithPaginationQueryHandler> _localizer;

        public TransportLogsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<TransportLogsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<TransportLogDto>> Handle(TransportLogsWithPaginationQuery request, CancellationToken cancellationToken)
        {

           var data = await _context.TransportLogs.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<TransportLog, TransportLogDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}