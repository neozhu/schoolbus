// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;
using CleanArchitecture.Blazor.Application.Features.TripReports.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Queries.GetById;

public class GetTripReportByIdQuery : ICacheableRequest<TripReportDto>
{
   public required int Id { get; set; }
   public string CacheKey => TripReportCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => TripReportCacheKey.MemoryCacheEntryOptions;
}

public class GetTripReportByIdQueryHandler :
     IRequestHandler<GetTripReportByIdQuery, TripReportDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetTripReportByIdQueryHandler> _localizer;

    public GetTripReportByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetTripReportByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<TripReportDto> Handle(GetTripReportByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.TripReports.ApplySpecification(new TripReportByIdSpecification(request.Id))
                     .ProjectTo<TripReportDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"TripReport with id: [{request.Id}] not found.");;
        return data;
    }
}
