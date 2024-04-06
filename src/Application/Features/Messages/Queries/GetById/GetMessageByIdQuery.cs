// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Messages.DTOs;
using CleanArchitecture.Blazor.Application.Features.Messages.Caching;
using CleanArchitecture.Blazor.Application.Features.Messages.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Messages.Queries.GetById;

public class GetMessageByIdQuery : ICacheableRequest<MessageDto>
{
   public required int Id { get; set; }
   public string CacheKey => MessageCacheKey.GetByIdCacheKey($"{Id}");
   public MemoryCacheEntryOptions? Options => MessageCacheKey.MemoryCacheEntryOptions;
}

public class GetMessageByIdQueryHandler :
     IRequestHandler<GetMessageByIdQuery, MessageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetMessageByIdQueryHandler> _localizer;

    public GetMessageByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetMessageByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<MessageDto> Handle(GetMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Messages.ApplySpecification(new MessageByIdSpecification(request.Id))
                     .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Message with id: [{request.Id}] not found.");;
        return data;
    }
}
