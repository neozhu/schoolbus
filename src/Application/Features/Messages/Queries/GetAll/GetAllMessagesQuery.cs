// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Messages.DTOs;
using CleanArchitecture.Blazor.Application.Features.Messages.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Messages.Queries.GetAll;

public class GetAllMessagesQuery : ICacheableRequest<IEnumerable<MessageDto>>
{
   public string CacheKey => MessageCacheKey.GetAllCacheKey;
   public MemoryCacheEntryOptions? Options => MessageCacheKey.MemoryCacheEntryOptions;
}

public class GetAllMessagesQueryHandler :
     IRequestHandler<GetAllMessagesQuery, IEnumerable<MessageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetAllMessagesQueryHandler> _localizer;

    public GetAllMessagesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetAllMessagesQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }

    public async Task<IEnumerable<MessageDto>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Messages
                     .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                     .AsNoTracking()
                     .ToListAsync(cancellationToken);
        return data;
    }
}


