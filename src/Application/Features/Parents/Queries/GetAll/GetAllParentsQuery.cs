// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Parents.DTOs;
using CleanArchitecture.Blazor.Application.Features.Parents.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Parents.Queries.GetAll;

public class GetAllParentsQuery : ICacheableRequest<IEnumerable<ParentDto>>
{
   public string CacheKey => ParentCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => ParentCacheKey.MemoryCacheEntryOptions;
}

public class GetAllParentsQueryHandler :
     IRequestHandler<GetAllParentsQuery, IEnumerable<ParentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllParentsQueryHandler> _localizer;

    public GetAllParentsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllParentsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<ParentDto>> Handle(GetAllParentsQuery request, CancellationToken cancellationToken)
    {
      
        var data = await _context.Parents
                     .ProjectTo<ParentDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


