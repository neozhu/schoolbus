// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;


namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Commands.Delete;

    public class DeleteTransportLogCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => TransportLogCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => TransportLogCacheKey.SharedExpiryTokenSource();
      public DeleteTransportLogCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteTransportLogCommandHandler : 
                 IRequestHandler<DeleteTransportLogCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteTransportLogCommandHandler> _localizer;
        public DeleteTransportLogCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteTransportLogCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteTransportLogCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement DeleteCheckedTransportLogsCommandHandler method 
            var items = await _context.TransportLogs.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new TransportLogDeletedEvent(item));
                _context.TransportLogs.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

