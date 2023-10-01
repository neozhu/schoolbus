// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Parents.DTOs;
using CleanArchitecture.Blazor.Application.Features.Parents.Caching;
using CleanArchitecture.Blazor.Application.Features.Parents.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Parents.Queries.Pagination;

public class ParentsWithPaginationQuery : ParentAdvancedFilter, ICacheableRequest<PaginatedData<ParentDto>>
{
    public override string ToString()
    {
        return $"{CurrentUser.UserId},Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => ParentCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => ParentCacheKey.MemoryCacheEntryOptions;
    public ParentAdvancedSpecification Specification => new ParentAdvancedSpecification(this);
}
    
public class ParentsWithPaginationQueryHandler :
         IRequestHandler<ParentsWithPaginationQuery, PaginatedData<ParentDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ParentsWithPaginationQueryHandler> _localizer;

        public ParentsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<ParentsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<ParentDto>> Handle(ParentsWithPaginationQuery request, CancellationToken cancellationToken)
        {
 
           var data = await _context.Parents.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Parent, ParentDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}