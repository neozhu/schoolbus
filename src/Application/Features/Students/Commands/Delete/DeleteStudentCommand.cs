// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Students.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.Delete;

    public class DeleteStudentCommand:  ICacheInvalidatorRequest<Result<int>>
    {
      public int[] Id {  get; }
      public string CacheKey => StudentCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => StudentCacheKey.SharedExpiryTokenSource();
      public DeleteStudentCommand(int[] id)
      {
        Id = id;
      }
    }

    public class DeleteStudentCommandHandler : 
                 IRequestHandler<DeleteStudentCommand, Result<int>>

    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteStudentCommandHandler> _localizer;
        public DeleteStudentCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteStudentCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var items = await _context.Students.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
			    // raise a delete domain event
				item.AddDomainEvent(new StudentDeletedEvent(item));
                _context.Students.Remove(item);
            }
            var result = await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(result);
        }

    }

