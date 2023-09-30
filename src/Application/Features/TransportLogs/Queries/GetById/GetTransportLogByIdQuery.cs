// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Queries.GetById;

public class GetTransportLogByIdQuery : ICacheableRequest<TransportLogDto>
{
   public required int Id { get; set; }
   public string CacheKey => TransportLogCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => TransportLogCacheKey.MemoryCacheEntryOptions;
}

public class GetTransportLogByIdQueryHandler :
   
     IRequestHandler<GetTransportLogByIdQuery, TransportLogDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetTransportLogByIdQueryHandler> _localizer;

    public GetTransportLogByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetTransportLogByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<TransportLogDto> Handle(GetTransportLogByIdQuery request, CancellationToken cancellationToken)
    {
    
        var data = await _context.TransportLogs.ApplySpecification(new TransportLogByIdSpecification(request.Id))
                     .ProjectTo<TransportLogDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"TransportLog with id: [{request.Id}] not found.");;
        return data;
    }
    
}
