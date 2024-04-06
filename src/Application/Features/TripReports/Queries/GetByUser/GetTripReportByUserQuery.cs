// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;
using CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Queries.GetById;

public class GetTripReportByUserQueryQuery : ICacheableRequest<List<TripReportToPlainDto>>
{
    public TripStatus? Status { get; set; }
    public UserProfile UserProfile { get; set; }
    public string CacheKey => TripReportCacheKey.GetByUserCacheKey($"{UserProfile.UserId},{Status}");
    public MemoryCacheEntryOptions? Options => TripReportCacheKey.MemoryCacheEntryOptions;
}

public class GetTripReportByUserQueryQueryHandler :
     IRequestHandler<GetTripReportByUserQueryQuery, List<TripReportToPlainDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetTripReportByUserQueryQueryHandler> _localizer;

    public GetTripReportByUserQueryQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetTripReportByUserQueryQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<List<TripReportToPlainDto>> Handle(GetTripReportByUserQueryQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.TripReports.ApplySpecification(new TripReportByUserSpecification(request.UserProfile, request.Status))
                     .ProjectTo<TripReportToPlainDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
        return data;
    }
}
