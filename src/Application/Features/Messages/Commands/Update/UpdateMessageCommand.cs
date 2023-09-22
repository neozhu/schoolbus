// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Messages.DTOs;
using CleanArchitecture.Blazor.Application.Features.Messages.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Messages.Commands.Update;

public class UpdateMessageCommand: ICacheInvalidatorRequest<Result<int>>
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
            CreateMap<MessageDto,UpdateMessageCommand>(MemberList.None);
            CreateMap<UpdateMessageCommand,Message>(MemberList.None);
        }
    }
}

    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateMessageCommandHandler> _localizer;
        public UpdateMessageCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateMessageCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
        {

           var item =await _context.Messages.FindAsync( new object[] { request.Id }, cancellationToken)?? throw new NotFoundException($"Message with id: [{request.Id}] not found.");;
           item = _mapper.Map(request, item);
		    // raise a update domain event
		   item.AddDomainEvent(new MessageUpdatedEvent(item));
           await _context.SaveChangesAsync(cancellationToken);
           return await Result<int>.SuccessAsync(item.Id);
        }
    }

