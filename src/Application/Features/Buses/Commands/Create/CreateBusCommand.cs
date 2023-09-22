// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Buses.Commands.Create;

public class CreateBusCommand: ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Plat Number")]
    public string? PlatNumber {get;set;} 
    [Description("Device Id")]
    public string? DeviceId {get;set;} 
    [Description("Status")]
    public string? Status {get;set;} 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Tenant Id")]
    public string? TenantId {get;set;} 

      public string CacheKey => BusCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => BusCacheKey.SharedExpiryTokenSource();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<BusDto, CreateBusCommand>().ReverseMap();
        }
    }
}
    
    public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateBusCommand> _localizer;
        public CreateBusCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateBusCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {
           // TODO: Implement CreateBusCommandHandler method 
           var dto = _mapper.Map<BusDto>(request);
           var item = _mapper.Map<Bus>(dto);
           // raise a create domain event
	       item.AddDomainEvent(new BusCreatedEvent(item));
           _context.Buses.Add(item);
           await _context.SaveChangesAsync(cancellationToken);
           return  await Result<int>.SuccessAsync(item.Id);
        }
    }

