// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Messages.DTOs;
using CleanArchitecture.Blazor.Application.Features.Messages.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Messages.Commands.Create;

public class CreateMessageCommand: ICacheInvalidatorRequest<Result<int>>
{
      [Description("Id")]
      public int Id { get; set; }
          [Description("From")]
    public string? From {get;set;} 
    [Description("To")]
    public string? To {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Content")]
    public string? Content {get;set;} 
    [Description("Sent Time")]
    public DateTime? SentTime {get;set;} 
    [Description("Read Time")]
    public DateTime? ReadTime {get;set;} 
    [Description("Status")]
    public string? Status {get;set;} 
    [Description("Tenant Id")]
    public string? TenantId {get;set;} 

      public string CacheKey => MessageCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => MessageCacheKey.SharedExpiryTokenSource();
    private class Mapping : Profile
    {
        public Mapping()
        {
             CreateMap<MessageDto,CreateMessageCommand>(MemberList.None);
             CreateMap<CreateMessageCommand,Message>(MemberList.None);
        }
    }
}
    
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateMessageCommand> _localizer;
        public CreateMessageCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateMessageCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
           var item = _mapper.Map<Message>(request);
           // raise a create domain event
	       item.AddDomainEvent(new MessageCreatedEvent(item));
           _context.Messages.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  await Result<int>.SuccessAsync(item.Id);
        }
    }

