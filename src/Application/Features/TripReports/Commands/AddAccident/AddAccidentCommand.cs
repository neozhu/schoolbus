// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.Accident;
public class AddAccidentCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int TripId { get; }
    public DateTime? ReportDateTime { get; set; } = DateTime.Now;
    public AccidentLevel Level { get; set; } = AccidentLevel.Trouble;
    public InfractionType? Infraction {get;set;}
    public string? Comments { get; set; }
    public string? Location { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string TenantId { get; set; }
    public int? StudentId { get; set; }
    public string CacheKey => TripReportCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TripReportCacheKey.SharedExpiryTokenSource();
    public AddAccidentCommand(int tripId, string tenantId)
    {
        TripId = tripId;
        TenantId = tenantId;
    }
}


public class GetOnOffCommandHandler :
              IRequestHandler<AddAccidentCommand, Result<int>>


{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetOnOffCommandHandler> _localizer;
    public GetOnOffCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<GetOnOffCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(AddAccidentCommand request, CancellationToken cancellationToken)
    {

        var item = new TripAccident()
        {
            ReportDateTime= request.ReportDateTime,
            Infraction =request.Infraction,
            TenantId = request.TenantId,
            TripId = request.TripId,
            Level = request.Level,
            Comments = request.Comments,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Location = request.Location,
            StudentId = request.StudentId,
            Status = "Pending"
        };

        _context.TripAccidents.Add(item);

        await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(item.Id);
    }


}

