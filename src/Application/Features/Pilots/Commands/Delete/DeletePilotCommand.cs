// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Pilots.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Pilots.Commands.Delete;

    public class DeletePilotCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => PilotCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => PilotCacheKey.SharedExpiryTokenSource();
      public DeletePilotCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeletePilotCommandHandler : 
                 IRequestHandler<DeletePilotCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeletePilotCommandHandler> _localizer;
        public DeletePilotCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeletePilotCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeletePilotCommand request, CancellationToken cancellationToken)
        {
     
            var items = await _context.Pilots.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new PilotDeletedEvent(item));
                _context.Pilots.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

