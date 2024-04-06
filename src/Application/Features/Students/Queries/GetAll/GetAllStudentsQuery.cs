// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Students.Queries.GetAll;

public class GetAllStudentsQuery : ICacheableRequest<IEnumerable<StudentDto>>
{
   public string CacheKey => StudentCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => StudentCacheKey.MemoryCacheEntryOptions;
}

public class GetAllStudentsQueryHandler :
     IRequestHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllStudentsQueryHandler> _localizer;

    public GetAllStudentsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllStudentsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
  
        var data = await _context.Students
                     .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


