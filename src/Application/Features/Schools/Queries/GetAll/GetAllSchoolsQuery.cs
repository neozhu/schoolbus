// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Schools.Queries.GetAll;

public class GetAllSchoolsQuery : ICacheableRequest<IEnumerable<SchoolDto>>
{
   public string CacheKey => SchoolCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => SchoolCacheKey.MemoryCacheEntryOptions;
}

public class GetAllSchoolsQueryHandler :
     IRequestHandler<GetAllSchoolsQuery, IEnumerable<SchoolDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllSchoolsQueryHandler> _localizer;

    public GetAllSchoolsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllSchoolsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<SchoolDto>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Schools
                     .ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


