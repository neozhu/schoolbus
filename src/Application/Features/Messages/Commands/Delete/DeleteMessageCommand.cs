// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Messages.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Messages.Commands.Delete;

    public class DeleteMessageCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => MessageCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => MessageCacheKey.SharedExpiryTokenSource();
      public DeleteMessageCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteMessageCommandHandler : 
                 IRequestHandler<DeleteMessageCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteMessageCommandHandler> _localizer;
        public DeleteMessageCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteMessageCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.Messages.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new MessageDeletedEvent(item));
                _context.Messages.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

