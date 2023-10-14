// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.DTOs;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;
using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.AddEdit;

public class AddEditTripReportCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Itinerary Id")]
    public int? ItineraryId { get; set; }
    [Description("Pilot Id")]
    public int? PilotId { get; set; }
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


    public string CacheKey => TripReportCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TripReportCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<TripReportDto, AddEditTripReportCommand>(MemberList.None);
            CreateMap<AddEditTripReportCommand, TripReport>(MemberList.None);
                //.ForMember(x => x.Tenant, y => y.MapFrom(x => default(Tenant)))
                //.ForMember(x => x.Itinerary, y => y.MapFrom(x => default(Itinerary)))
                //.ForMember(x => x.Pilot, y => y.MapFrom(x => default(Pilot)));

        }
    }
}

public class AddEditTripReportCommandHandler : IRequestHandler<AddEditTripReportCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<AddEditTripReportCommandHandler> _localizer;
    public AddEditTripReportCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<AddEditTripReportCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(AddEditTripReportCommand request, CancellationToken cancellationToken)
    {
        var totalstudents = await _context.Students.CountAsync(x => x.TenantId == request.TenantId && x.ItineraryId == request.ItineraryId);
        if (request.Id > 0)
        {
            var onboardstudents = await _context.TripLogs.Where(x => x.TripId == request.Id).Select(x => x.StudentId).Distinct().CountAsync();
            var item = await _context.TripReports.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"TripReport with id: [{request.Id}] not found.");
            item = _mapper.Map(request, item);
            item.OnBoard = onboardstudents;
            item.NotOnBoard = totalstudents - onboardstudents;
            // raise a update domain event
            item.AddDomainEvent(new TripReportUpdatedEvent(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            
            var item = _mapper.Map<TripReport>(request);
            item.NotOnBoard = totalstudents;
            item.OnBoard = 0;
            // raise a create domain event
            item.AddDomainEvent(new TripReportCreatedEvent(item));
            _context.TripReports.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }

    }
}

