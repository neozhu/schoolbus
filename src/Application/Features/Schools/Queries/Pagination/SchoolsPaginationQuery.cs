// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.Caching;
using CleanArchitecture.Blazor.Application.Features.Schools.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Schools.Queries.Pagination;

public class SchoolsWithPaginationQuery : SchoolAdvancedFilter, ICacheableRequest<PaginatedData<SchoolDto>>
{
    public override string ToString()
    {
        return $"{CurrentUser.UserId}:Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => SchoolCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => SchoolCacheKey.MemoryCacheEntryOptions;
    public SchoolAdvancedSpecification Specification => new SchoolAdvancedSpecification(this);
}
    
public class SchoolsWithPaginationQueryHandler :
         IRequestHandler<SchoolsWithPaginationQuery, PaginatedData<SchoolDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SchoolsWithPaginationQueryHandler> _localizer;

        public SchoolsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<SchoolsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<SchoolDto>> Handle(SchoolsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Schools.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<School, SchoolDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}