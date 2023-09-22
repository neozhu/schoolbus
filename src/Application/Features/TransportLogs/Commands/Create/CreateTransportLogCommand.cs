// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Commands.Create;

public class CreateTransportLogCommand: ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Student Id")]
    public int StudentId {get;set;} 
    [Description("Itinerary Id")]
    public int ItineraryId {get;set;} 
    [Description("Device Id")]
    public string? DeviceId {get;set;} 
    [Description("Swipe Date Time")]
    public DateTime SwipeDateTime {get;set;} 
    [Description("Location")]
    public string? Location {get;set;} 
    [Description("Longitude")]
    public double? Longitude {get;set;} 
    [Description("Latitude")]
    public double? Latitude {get;set;} 
    [Description("Check Type")]
    public string? CheckType {get;set;} 
    [Description("Up Or Down")]
    public string? UpOrDown {get;set;} 
    [Description("Comments")]
    public string? Comments {get;set;} 
    [Description("Tenant Id")]
    public string? TenantId {get;set;} 

      public string CacheKey => TransportLogCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => TransportLogCacheKey.SharedExpiryTokenSource();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<CreateTransportLogCommand, TransportLog>(MemberList.None);
        }
    }
}
    
    public class CreateTransportLogCommandHandler : IRequestHandler<CreateTransportLogCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateTransportLogCommand> _localizer;
        public CreateTransportLogCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateTransportLogCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateTransportLogCommand request, CancellationToken cancellationToken)
        {
    
           var item = _mapper.Map<TransportLog>(request);
           // raise a create domain event
	       item.AddDomainEvent(new TransportLogCreatedEvent(item));
           _context.TransportLogs.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  await Result<int>.SuccessAsync(item.Id);
        }
    }

