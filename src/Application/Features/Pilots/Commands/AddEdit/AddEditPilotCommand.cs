// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.Caching;
namespace CleanArchitecture.Blazor.Application.Features.Pilots.Commands.AddEdit;

public class AddEditPilotCommand: ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Last Name")]
    public string? LastName {get;set;} 
    [Description("First Name")]
    public string? FirstName {get;set;} 
    [Description("Profile Picture")]
    public string? ProfilePicture {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Status")]
    public string? Status {get;set;} 
    [Description("Tenant Id")]
    public string? TenantId {get;set;} 


      public string CacheKey => PilotCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => PilotCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PilotDto,AddEditPilotCommand>(MemberList.None);
            CreateMap<AddEditPilotCommand, Pilot>(MemberList.None);
        }
    }
}

    public class AddEditPilotCommandHandler : IRequestHandler<AddEditPilotCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditPilotCommandHandler> _localizer;
        public AddEditPilotCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditPilotCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditPilotCommand request, CancellationToken cancellationToken)
        {
            if (request.Id > 0)
            {
                var item = await _context.Pilots.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Pilot with id: [{request.Id}] not found.");
                item = _mapper.Map(request, item);
				// raise a update domain event
				item.AddDomainEvent(new PilotUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = _mapper.Map<Pilot>(request);
                // raise a create domain event
				item.AddDomainEvent(new PilotCreatedEvent(item));
                _context.Pilots.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
           
        }
    }

