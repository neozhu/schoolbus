// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;
namespace CleanArchitecture.Blazor.Application.Features.Buses.Commands.AddEdit;

public class AddEditBusCommand: ICacheInvalidatorRequest<Result<int>>
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
            CreateMap<BusDto, AddEditBusCommand>(MemberList.None);
            CreateMap<AddEditBusCommand, Bus>(MemberList.None);
        }
    }
}

    public class AddEditBusCommandHandler : IRequestHandler<AddEditBusCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditBusCommandHandler> _localizer;
        public AddEditBusCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditBusCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditBusCommand request, CancellationToken cancellationToken)
        {

            if (request.Id > 0)
            {
                var item = await _context.Buses.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Bus with id: [{request.Id}] not found.");
                item = _mapper.Map(request, item);
				// raise a update domain event
				item.AddDomainEvent(new BusUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = _mapper.Map<Bus>(request);
                // raise a create domain event
				item.AddDomainEvent(new BusCreatedEvent(item));
                _context.Buses.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
           
        }
    }

