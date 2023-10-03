// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Queries.GetAll;

public class GetAllTripReportsQuery : ICacheableRequest<IEnumerable<TripReportDto>>
{
   public string CacheKey => TripReportCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => TripReportCacheKey.MemoryCacheEntryOptions;
}

public class GetAllTripReportsQueryHandler :
     IRequestHandler<GetAllTripReportsQuery, IEnumerable<TripReportDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllTripReportsQueryHandler> _localizer;

    public GetAllTripReportsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllTripReportsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<TripReportDto>> Handle(GetAllTripReportsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.TripReports
                     .ProjectTo<TripReportDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


