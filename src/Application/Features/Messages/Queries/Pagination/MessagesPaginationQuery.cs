// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Messages.DTOs;
using CleanArchitecture.Blazor.Application.Features.Messages.Caching;
using CleanArchitecture.Blazor.Application.Features.Messages.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Messages.Queries.Pagination;

public class MessagesWithPaginationQuery : MessageAdvancedFilter, ICacheableRequest<PaginatedData<MessageDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => MessageCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => MessageCacheKey.MemoryCacheEntryOptions;
    public MessageAdvancedSpecification Specification => new MessageAdvancedSpecification(this);
}
    
public class MessagesWithPaginationQueryHandler :
         IRequestHandler<MessagesWithPaginationQuery, PaginatedData<MessageDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<MessagesWithPaginationQueryHandler> _localizer;

        public MessagesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<MessagesWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<MessageDto>> Handle(MessagesWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Messages.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Message, MessageDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}