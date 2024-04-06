// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;
using CleanArchitecture.Blazor.Application.Features.Buses.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Buses.Queries.GetById;

public class GetBusByIdQuery : ICacheableRequest<BusDto>
{
   public required int Id { get; set; }
   public string CacheKey => BusCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => BusCacheKey.MemoryCacheEntryOptions;
}

public class GetBusByIdQueryHandler :
     IRequestHandler<GetBusByIdQuery, BusDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetBusByIdQueryHandler> _localizer;

    public GetBusByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetBusByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<BusDto> Handle(GetBusByIdQuery request, CancellationToken cancellationToken)
    {
 
        var data = await _context.Buses.ApplySpecification(new BusByIdSpecification(request.Id))
                     .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Bus with id: [{request.Id}] not found.");;
        return data;
    }
}
