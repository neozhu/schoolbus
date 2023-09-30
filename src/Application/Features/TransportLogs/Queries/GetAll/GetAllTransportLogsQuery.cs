// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Queries.GetAll;

public class GetAllTransportLogsQuery : ICacheableRequest<IEnumerable<TransportLogDto>>
{
   public string CacheKey => TransportLogCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => TransportLogCacheKey.MemoryCacheEntryOptions;
}
public class GetTransportLogByStudentIdQuery : ICacheableRequest<List<TransportLogDto>>
{
    public int StudentId { get; set; }
    public string CacheKey => TransportLogCacheKey.GetByStudentIdCacheKey($"{StudentId}");
    public MemoryCacheEntryOptions? Options => TransportLogCacheKey.MemoryCacheEntryOptions;
    public GetTransportLogByStudentIdQuery(int studentId)
    {
        StudentId = studentId;
    }
}
public class GetOnBoardTransportLogsQuery : ICacheableRequest<IEnumerable<TransportLogDto>>
{
    public required int ItineraryId { get; set; }
    public string CacheKey => TransportLogCacheKey.GetOnBoardCacheKey($"{ItineraryId}");
    public MemoryCacheEntryOptions? Options => TransportLogCacheKey.MemoryCacheEntryOptions;
}
public class GetAllTransportLogsQueryHandler :
      IRequestHandler<GetTransportLogByStudentIdQuery, List<TransportLogDto>>,
     IRequestHandler<GetAllTransportLogsQuery, IEnumerable<TransportLogDto>>,
     IRequestHandler<GetOnBoardTransportLogsQuery, IEnumerable<TransportLogDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllTransportLogsQueryHandler> _localizer;

    public GetAllTransportLogsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllTransportLogsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<List<TransportLogDto>> Handle(GetTransportLogByStudentIdQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.TransportLogs.ApplySpecification(new TransportLogByStudentIdSpecification(request.StudentId, 4))
                     .ProjectTo<TransportLogDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
        return data;
    }
    public async Task<IEnumerable<TransportLogDto>> Handle(GetOnBoardTransportLogsQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.TransportLogs.ApplySpecification(new TransportLogByItineraryIdOnboardSpecification(request.ItineraryId))
                     .ProjectTo<TransportLogDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
    public async Task<IEnumerable<TransportLogDto>> Handle(GetAllTransportLogsQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.TransportLogs
                     .ProjectTo<TransportLogDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


