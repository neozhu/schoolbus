// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;


namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Commands.Delete;

public class CheckoutTransportLogCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int Id { get; }

    public string? Location { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }
    public string CacheKey => TransportLogCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TransportLogCacheKey.SharedExpiryTokenSource();
    public CheckoutTransportLogCommand(int id)
    {
        Id = id;
    }
}

public class CheckoutTransportLogCommandHandler :
             IRequestHandler<CheckoutTransportLogCommand, Result<int>>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<CheckoutTransportLogCommandHandler> _localizer;
    public CheckoutTransportLogCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<CheckoutTransportLogCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(CheckoutTransportLogCommand request, CancellationToken cancellationToken)
    {

        var item = await _context.TransportLogs.FindAsync(request.Id);
        item.Done = true;
        var checkout = new TransportLog()
        {
            CheckType = "Manual",
            DeviceId = item.DeviceId,
            Done = true,
            ItineraryId = item.ItineraryId,
            Itinerary = null,
            Comments = "Manual Checkout!",
            Latitude = request.Latitude,
            Location = request.Location,
            Longitude = request.Longitude,
            RefId = item.Id,
            StudentId = item.StudentId,
            Student = null,
            SwipeDateTime = DateTime.Now,
            UpOrDown = "Down",
            TenantId = item.TenantId,
            Tenant = null,

        };
        checkout.AddDomainEvent(new TransportLogCreatedEvent(item));
        _context.TransportLogs.Add(checkout);
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

