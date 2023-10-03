// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TripReports.Caching;


namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.Delete;

    public class DeleteTripReportCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => TripReportCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => TripReportCacheKey.SharedExpiryTokenSource();
      public DeleteTripReportCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteTripReportCommandHandler : 
                 IRequestHandler<DeleteTripReportCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteTripReportCommandHandler> _localizer;
        public DeleteTripReportCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteTripReportCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteTripReportCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.TripReports.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new TripReportDeletedEvent(item));
                _context.TripReports.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

