// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;


namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.Done;

public class DoneTripReportCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int Id { get; }
    public string? Comments { get; set; }
    public string CacheKey => TripReportCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => TripReportCacheKey.SharedExpiryTokenSource();
    public DoneTripReportCommand(int id)
    {
        Id = id;
    }
}

public class DoneTripReportCommandHandler :
             IRequestHandler<DoneTripReportCommand, Result<int>>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<DoneTripReportCommandHandler> _localizer;
    public DoneTripReportCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<DoneTripReportCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(DoneTripReportCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.TripReports.FindAsync(request.Id, cancellationToken);
        item.ReportDate = DateTime.Now;
        item.Status = TripStatus.Done;
        item.Comments = request.Comments;
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

