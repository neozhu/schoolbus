// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Parents.DTOs;
using CleanArchitecture.Blazor.Application.Features.Parents.Caching;
using CleanArchitecture.Blazor.Application.Features.Parents.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Parents.Queries.GetById;

public class GetParentByIdQuery : ICacheableRequest<ParentDto>
{
   public required int Id { get; set; }
   public string CacheKey => ParentCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => ParentCacheKey.MemoryCacheEntryOptions;
}

public class GetParentByIdQueryHandler :
     IRequestHandler<GetParentByIdQuery, ParentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetParentByIdQueryHandler> _localizer;

    public GetParentByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetParentByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<ParentDto> Handle(GetParentByIdQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Parents.ApplySpecification(new ParentByIdSpecification(request.Id))
                     .ProjectTo<ParentDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Parent with id: [{request.Id}] not found.");;
        return data;
    }
}
