// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.Caching;
using CleanArchitecture.Blazor.Application.Features.Pilots.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Pilots.Queries.GetById;

public class GetPilotByIdQuery : ICacheableRequest<PilotDto>
{
   public required int Id { get; set; }
   public string CacheKey => PilotCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => PilotCacheKey.MemoryCacheEntryOptions;
}

public class GetPilotByIdQueryHandler :
     IRequestHandler<GetPilotByIdQuery, PilotDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetPilotByIdQueryHandler> _localizer;

    public GetPilotByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetPilotByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<PilotDto> Handle(GetPilotByIdQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Pilots.ApplySpecification(new PilotByIdSpecification(request.Id))
                     .ProjectTo<PilotDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Pilot with id: [{request.Id}] not found.");;
        return data;
    }
}
