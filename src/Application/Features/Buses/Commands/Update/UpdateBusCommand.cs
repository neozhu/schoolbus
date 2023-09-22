// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.ComponentModel;
using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Buses.Commands.Update;

public class UpdateBusCommand: ICacheInvalidatorRequest<Result<int>>
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
            CreateMap<BusDto, UpdateBusCommand>().ReverseMap();
        }
    }
}

    public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateBusCommandHandler> _localizer;
        public UpdateBusCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateBusCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
        {
           // TODO: Implement UpdateBusCommandHandler method 
           var item =await _context.Buses.FindAsync( new object[] { request.Id }, cancellationToken)?? throw new NotFoundException($"Bus with id: [{request.Id}] not found.");;
           var dto = _mapper.Map<BusDto>(request);
           item = _mapper.Map(dto, item);
		    // raise a update domain event
		   item.AddDomainEvent(new BusUpdatedEvent(item));
           await _context.SaveChangesAsync(cancellationToken);
           return await Result<int>.SuccessAsync(item.Id);
        }
    }

