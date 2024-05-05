// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;
using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.Start;

public class StartTripReportCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Bus Id")]
    public int? BusId { get; set; }
    [Description("Itinerary Id")]
    public int? ItineraryId { get; set; }
    //[Description("Pilot Id")]
    //public int? PilotId { get; set; }
    [Description("Driver Id")]
    public string? DriverId { get; set; }
    [Description("Plat Number")]
    public string? PlatNumber { get; set; }
    [Description("On Board")]
    public int OnBoard { get; set; }
    [Description("Not On Board")]
    public int NotOnBoard { get; set; }
    [Description("Departure Date")]
    public DateTime? DepartureDate { get; set; }
    [Description("Report Date")]
    public DateTime? ReportDate { get; set; }
    [Description("Status")]
    public TripStatus Status { get; set; } = TripStatus.Runing;
    [Description("Comments")]
    public string? Comments { get; set; }
    [Description("Tenant Id")]
    public string? TenantId { get; set; }

    public string? StartingStation { get; set; }
    public string? TerminalStation { get; set; }

    public string CacheKey => TripReportCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TripReportCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TripReportToPlainDto, StartTripReportCommand>(MemberList.None);
            CreateMap<StartTripReportCommand, TripReport>(MemberList.None);
              

        }
    }
}

public class StartTripReportCommandHandler : IRequestHandler<StartTripReportCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<StartTripReportCommandHandler> _localizer;
    public StartTripReportCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<StartTripReportCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(StartTripReportCommand request, CancellationToken cancellationToken)
    {
        var totalstudents = await _context.Students.CountAsync(x => x.TenantId == request.TenantId && x.ItineraryId == request.ItineraryId);
        if (totalstudents == 0)
        {
            return await Result<int>.FailureAsync(new string[] {$"No students have been assigned to Itinerary:{request.ItineraryId} yet." });
        }
        var tripreport = await _context.TripReports.Where(x => x.DriverId == request.DriverId && x.ItineraryId == request.ItineraryId && x.Status == TripStatus.Runing).FirstOrDefaultAsync();
        if (tripreport == null) {
            var item = _mapper.Map<TripReport>(request);
            item.NotOnBoard = totalstudents;
            item.OnBoard = 0;
            // raise a create domain event
            item.AddDomainEvent(new TripReportCreatedEvent(item));
            _context.TripReports.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            return await Result<int>.SuccessAsync(tripreport.Id);
        }
        

    }
}

