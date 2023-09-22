// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Schools.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Schools.Commands.Delete;

    public class DeleteSchoolCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => SchoolCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => SchoolCacheKey.SharedExpiryTokenSource();
      public DeleteSchoolCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteSchoolCommandHandler : 
                 IRequestHandler<DeleteSchoolCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteSchoolCommandHandler> _localizer;
        public DeleteSchoolCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteSchoolCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement DeleteCheckedSchoolsCommandHandler method 
            var items = await _context.Schools.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new SchoolDeletedEvent(item));
                _context.Schools.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

