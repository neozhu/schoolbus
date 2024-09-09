// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;
using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.GetOnOff;
public class GetOffMenualCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int Id { get; }
    public string? Comments { get; set; }
    public string? Location { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string CacheKey => TripReportCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TripReportCacheKey.SharedExpiryTokenSource();
    public GetOffMenualCommand(int id)
    {
        Id = id;
    }
}
public class GetOnOffCommand : ICacheInvalidatorRequest<Result<int>>
{
    public string TenantId { get; set; }
    public int TripId { get; set; }
    public int StudentId { get; set; }
    public string? UID { get; set; }
    public bool Manual { get; set; }
    public string? Comments { get; set; }
    public string? Location { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string CacheKey => TripReportCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TripReportCacheKey.SharedExpiryTokenSource();
    public GetOnOffCommand()
    {

    }
    public GetOnOffCommand(int tripId, int studentId, string tenantId)
    {
        TripId = tripId;
        StudentId = studentId;
        TenantId = tenantId;
    }
}

public class GetOnOffCommandHandler :
              IRequestHandler<GetOffMenualCommand, Result<int>>,
             IRequestHandler<GetOnOffCommand, Result<int>>

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
    public async Task<Result<int>> Handle(GetOffMenualCommand request, CancellationToken cancellationToken)
    {
        var geton = await _context.TripLogs.FindAsync(request.Id, cancellationToken);
        var trip = await _context.TripReports.FindAsync(geton.TripId, cancellationToken);

        if (geton is not null)
        {
            geton.Manual2 = true;
            geton.Comments2 = request.Comments;
            geton.Location2 = request.Location;
            geton.Latitude2 = request.Latitude;
            geton.Longitude2 = request.Longitude;
            geton.GetOffDateTime2 = DateTime.Now;
            geton.AddDomainEvent(new TripLogCreatedEvent(geton));

            if (trip is not null)
            {
                var getonitem = new TransportLog() { CheckType = "Manual", Done = false, UpOrDown = "Get On", StudentId = geton.StudentId ?? 0, ItineraryId = trip.ItineraryId ?? 0, TenantId = trip.TenantId, Latitude = request.Latitude, Longitude = request.Longitude, Location = request.Location, SwipeDateTime = DateTime.Now, Comments = request.Comments, DeviceId = trip.Itinerary?.Bus.DeviceId };
                var getonrec = await _context.TransportLogs.Where(x => x.ItineraryId == trip.ItineraryId && x.StudentId == geton.StudentId && x.SwipeDateTime.Date == DateTime.Now.Date && x.Done == false).FirstOrDefaultAsync();
                if (getonrec is not null)
                {
                    getonrec.Done = true;
                    getonitem.UpOrDown = "Get Off";
                    getonitem.RefId = getonrec.Id;
                    getonitem.Done = true;
                }
                getonitem.AddDomainEvent(new TransportLogCreatedEvent(getonitem));
                _context.TransportLogs.Add(getonitem);

                var onboard = await _context.TripLogs.Where(x => x.TripId == trip.Id).Select(x => x.StudentId).Distinct().CountAsync();
                var total = await _context.Students.Where(x => x.ItineraryId == trip.ItineraryId && x.TenantId == trip.TenantId).CountAsync();
                trip.OnBoard = onboard;
                trip.NotOnBoard = total - onboard;
            }


        }



        await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(geton.Id);
    }
    public async Task<Result<int>> Handle(GetOnOffCommand request, CancellationToken cancellationToken)
    {
        if (request.StudentId == 0 && !string.IsNullOrEmpty(request.UID))
        {
            var student = await _context.Students.Where(x => x.UID == request.UID && x.TenantId == request.TenantId).FirstOrDefaultAsync();
            if (student is null)
            {
                return await Result<int>.FailureAsync(new string[] { $"not found student with {request.UID}" });
            }
            else
            {
                request.StudentId = student.Id;
            }
        }

        var trip = await _context.TripReports.FindAsync(request.TripId, cancellationToken);
        if (trip is null)
        {
            return await Result<int>.FailureAsync(new string[] { $"not found router {request.TripId}" });
        }
        var geton = await _context.TripLogs.Where(x => x.TripId == request.TripId && x.StudentId == request.StudentId && x.GetOffDateTime2 == null).FirstOrDefaultAsync(cancellationToken);
        if (geton is null)
        {
            geton = new TripLog()
            {
                TripId = request.TripId,
                TenantId = request.TenantId,
                StudentId = request.StudentId,
                UID = request.UID,
                GetOnDateTime = DateTime.Now,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Location = request.Location,
                Manual = request.Manual,
                Comments = request.Comments
            };
            geton.AddDomainEvent(new TripLogCreatedEvent(geton));
            _context.TripLogs.Add(geton);

            var getonitem = new TransportLog() { CheckType = (request.Manual ? "Manual" : "Automatic"), Done = false, UpOrDown = "On", StudentId = request.StudentId, ItineraryId = trip.ItineraryId ?? 0, TenantId = trip.TenantId, Latitude = request.Latitude, Longitude = request.Longitude, Location = request.Location, SwipeDateTime = DateTime.Now, Comments = request.Comments, DeviceId = trip.Itinerary?.Bus.DeviceId };
            getonitem.AddDomainEvent(new TransportLogCreatedEvent(getonitem));
            _context.TransportLogs.Add(getonitem);
        }
        else
        {
            var diff = geton.GetOnDateTime - DateTime.Now;
            if (diff.Value.TotalSeconds <= -15)
            {
                geton.Manual2 = request.Manual;
                geton.Comments2 = request.Comments;
                geton.Location2 = request.Location;
                geton.Latitude2 = request.Latitude;
                geton.Longitude2 = request.Longitude;
                geton.GetOffDateTime2 = DateTime.Now;

                var getonitem = new TransportLog() { CheckType = (request.Manual ? "Manual" : "Automatic"), Done = false, UpOrDown = "Off", StudentId = request.StudentId, ItineraryId = trip.ItineraryId ?? 0, TenantId = trip.TenantId, Latitude = request.Latitude, Longitude = request.Longitude, Location = request.Location, SwipeDateTime = DateTime.Now, Comments = request.Comments, DeviceId = trip.Itinerary?.Bus.DeviceId };
                getonitem.AddDomainEvent(new TransportLogCreatedEvent(getonitem));
                _context.TransportLogs.Add(getonitem);

            }

        }


        await _context.SaveChangesAsync(cancellationToken);

        var onboard = await _context.TripLogs.Where(x => x.TripId == trip.Id).Select(x => x.StudentId).Distinct().CountAsync();
        var total = await _context.Students.Where(x => x.ItineraryId == trip.ItineraryId && x.TenantId == trip.TenantId).CountAsync();
        await _context.TripReports.Where(x => x.Id == request.TripId).ExecuteUpdateAsync(x => x.SetProperty(x => x.OnBoard, onboard).SetProperty(x => x.NotOnBoard, total - onboard));

        return await Result<int>.SuccessAsync(geton.Id);
    }

}

