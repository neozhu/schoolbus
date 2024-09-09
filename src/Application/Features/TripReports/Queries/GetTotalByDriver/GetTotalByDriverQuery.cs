// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Queries.GetAll;

public class GetTotalByDriverQuery : ICacheableRequest<int>
{
    public required string DriverId { get; set; }
    public string CacheKey => TripReportCacheKey.GetTotalTripCacheKey($"{DriverId}");
   public MemoryCacheEntryOptions? Options => TripReportCacheKey.MemoryCacheEntryOptions;
}

public class GetTotalByDriverQueryHandler :
     IRequestHandler<GetTotalByDriverQuery, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetTotalByDriverQueryHandler> _localizer;

    public GetTotalByDriverQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetTotalByDriverQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<int> Handle(GetTotalByDriverQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.TripReports.CountAsync(x => x.DriverId == request.DriverId);
        return data;
    }
}


