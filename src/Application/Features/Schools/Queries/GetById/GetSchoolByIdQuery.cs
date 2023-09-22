// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.Caching;
using CleanArchitecture.Blazor.Application.Features.Schools.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Schools.Queries.GetById;

public class GetSchoolByIdQuery : ICacheableRequest<SchoolDto>
{
   public required int Id { get; set; }
   public string CacheKey => SchoolCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => SchoolCacheKey.MemoryCacheEntryOptions;
}

public class GetSchoolByIdQueryHandler :
     IRequestHandler<GetSchoolByIdQuery, SchoolDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetSchoolByIdQueryHandler> _localizer;

    public GetSchoolByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetSchoolByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<SchoolDto> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Schools.ApplySpecification(new SchoolByIdSpecification(request.Id))
                     .ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"School with id: [{request.Id}] not found.");;
        return data;
    }
}
