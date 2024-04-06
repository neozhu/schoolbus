// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Commands.Delete;

    public class DeleteItineraryCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => ItineraryCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => ItineraryCacheKey.SharedExpiryTokenSource();
      public DeleteItineraryCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteItineraryCommandHandler : 
                 IRequestHandler<DeleteItineraryCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteItineraryCommandHandler> _localizer;
        public DeleteItineraryCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteItineraryCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteItineraryCommand request, CancellationToken cancellationToken)
        {
      
            var items = await _context.Itineraries.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new ItineraryDeletedEvent(item));
                _context.Itineraries.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

