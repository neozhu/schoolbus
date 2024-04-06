// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Buses.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Buses.Commands.Delete;

    public class DeleteBusCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => BusCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => BusCacheKey.SharedExpiryTokenSource();
      public DeleteBusCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteBusCommandHandler : 
                 IRequestHandler<DeleteBusCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteBusCommandHandler> _localizer;
        public DeleteBusCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteBusCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement DeleteCheckedBusesCommandHandler method 
            var items = await _context.Buses.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new BusDeletedEvent(item));
                _context.Buses.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

