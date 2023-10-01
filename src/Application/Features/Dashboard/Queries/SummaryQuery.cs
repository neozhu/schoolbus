using CleanArchitecture.Blazor.Application.Features.Dashboard.Dto;
using CleanArchitecture.Blazor.Application.Features.Dashboard.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Dashboard.Queries;
public class SummaryQuery:ICacheableRequest<AggregationResult>
{
    public required UserProfile UserProfile { get; set; }
    public string CacheKey => "SummaryQuery:" + UserProfile.UserId;
    public MemoryCacheEntryOptions? Options => new MemoryCacheEntryOptions() { SlidingExpiration=new TimeSpan(0,30,0) };
}
public class SummaryQueryHandler :
     IRequestHandler<SummaryQuery, AggregationResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SummaryQueryHandler> _localizer;

    public SummaryQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<SummaryQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<AggregationResult> Handle(SummaryQuery request, CancellationToken cancellationToken)
    {
        var totalbuses = await _context.Buses.ApplySpecification(new SummaryBusSpecification(request.UserProfile))
                     .CountAsync();

        var totalschools = await _context.Schools.ApplySpecification(new SummarySchoolSpecification(request.UserProfile))
                     .CountAsync();
        var totalparents = await _context.Parents.ApplySpecification(new SummaryParentSpecification(request.UserProfile))
                     .CountAsync();
        var totalstudents = await _context.Students.ApplySpecification(new SummaryStudentSpecification(request.UserProfile))
                     .CountAsync();
        var totalpilots = await _context.Pilots.ApplySpecification(new SummaryPilotSpecification(request.UserProfile))
                     .CountAsync();
        var totaltransportlogs = await _context.TransportLogs.ApplySpecification(new SummaryTransportLogSpecification(request.UserProfile))
                     .CountAsync();
        var totalitineraries = await _context.Itineraries.ApplySpecification(new SummaryItinerarySpecification(request.UserProfile))
                     .CountAsync();
        var summary= new SummaryDto() { TotalBuses = totalbuses,TotalSchools= totalschools,TotalParents= totalparents,TotalStudents = totalstudents,TotalPilots= totalpilots, TotalItineraries=totalitineraries, TotalTransportLogs=totaltransportlogs };

        var totalofMonth = await _context.TransportLogs.GroupBy(x => new { x.SwipeDateTime.Year, x.SwipeDateTime.Month }).Select(g => new TotalOfMonthDto { Count = g.Count(), YearMonth = $"{g.Key.Year}-{g.Key.Month}" }).ToListAsync();

        var totalofStudent = await _context.Students.GroupBy(x => new { x.School.Name }).Select(g => new TotalOfStudentDto { SchoolName = g.Key.Name, Count = g.Count() }).ToListAsync();
        var totalofTransport= await _context.TransportLogs.GroupBy(x => new { x.Itinerary.Name }).Select(g => new TotalOfTransportDto { ItinerayName = g.Key.Name, Count = g.Count() }).ToListAsync();

        return new AggregationResult { Summary = summary, TotalOfMonth = totalofMonth, TotalOfStudent = totalofStudent, TotalOfTransport = totalofTransport };

    }
}