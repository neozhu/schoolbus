// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Parents.Caching;


namespace CleanArchitecture.Blazor.Application.Features.Parents.Commands.Delete;

public class DeleteParentCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int[] Id { get; }
    public string CacheKey => ParentCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => ParentCacheKey.SharedExpiryTokenSource();
    public DeleteParentCommand(int[] id)
    {
        Id = id;
    }
}
public class RemoveAllKidsCommand : ICacheInvalidatorRequest<Result<int>>
{
    public int ParentId { get; }
    public string CacheKey => ParentCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => ParentCacheKey.SharedExpiryTokenSource();
    public RemoveAllKidsCommand(int parentId)
    {
        ParentId = parentId;
    }
}

public class DeleteParentCommandHandler :
            IRequestHandler<RemoveAllKidsCommand, Result<int>>,
             IRequestHandler<DeleteParentCommand, Result<int>>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<DeleteParentCommandHandler> _localizer;
    public DeleteParentCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<DeleteParentCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(DeleteParentCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.Parents.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
            // raise a delete domain event
            item.AddDomainEvent(new ParentDeletedEvent(item));
            _context.Parents.Remove(item);
        }
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }
    public async Task<Result<int>> Handle(RemoveAllKidsCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.ParentStudents.Where(x => x.ParentsId==request.ParentId).ToListAsync(cancellationToken);
        if(items.Any())
        {
            _context.ParentStudents.RemoveRange(items);
        }
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

