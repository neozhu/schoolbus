// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Caching;
namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Commands.AddEdit;

public class AddEditItineraryCommand: ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name {get;set;} = String.Empty; 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Bus Id")]
    public int BusId {get;set;} 
    [Description("Pilot Id")]
    public int PilotId {get;set;} 
    [Description("School Id")]
    public int SchoolId {get;set;} 
    [Description("First Time")]
    public string? FirstTime {get;set;} 
    [Description("Last Time")]
    public string? LastTime {get;set;} 
    [Description("Starting Station")]
    public string? StartingStation {get;set;} 
    [Description("Terminal Station")]
    public string? TerminalStation {get;set;} 
    [Description("Disabled")]
    public bool Disabled {get;set;} 
    [Description("Tenant Id")]
    public string? TenantId {get;set;} 


      public string CacheKey => ItineraryCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => ItineraryCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddEditItineraryCommand, Itinerary>(MemberList.None);
        }
    }
}

    public class AddEditItineraryCommandHandler : IRequestHandler<AddEditItineraryCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditItineraryCommandHandler> _localizer;
        public AddEditItineraryCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditItineraryCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditItineraryCommand request, CancellationToken cancellationToken)
        {

            if (request.Id > 0)
            {
                var item = await _context.Itineraries.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Itinerary with id: [{request.Id}] not found.");
                item = _mapper.Map(request, item);
				// raise a update domain event
				item.AddDomainEvent(new ItineraryUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = _mapper.Map<Itinerary>(request);
                // raise a create domain event
				item.AddDomainEvent(new ItineraryCreatedEvent(item));
                _context.Itineraries.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
           
        }
    }

