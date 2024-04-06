// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Specifications;
using CleanArchitecture.Blazor.Application.Features.Buses.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Buses.Queries.Export;

public class ExportBusesQuery : BusAdvancedFilter, IRequest<Result<byte[]>>
{
      public BusAdvancedSpecification Specification => new BusAdvancedSpecification(this);
}
    
public class ExportBusesQueryHandler :
         IRequestHandler<ExportBusesQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportBusesQueryHandler> _localizer;
        private readonly BusDto _dto = new();
        public ExportBusesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportBusesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportBusesQuery request, CancellationToken cancellationToken)
        {
      
            var data = await _context.Buses.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<BusDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<BusDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.PlatNumber)],item => item.PlatNumber}, 
{_localizer[_dto.GetMemberDescription(x=>x.DeviceId)],item => item.DeviceId}, 
{_localizer[_dto.GetMemberDescription(x=>x.Status)],item => item.Status}, 
{_localizer[_dto.GetMemberDescription(x=>x.Description)],item => item.Description}, 
{_localizer[_dto.GetMemberDescription(x=>x.TenantId)],item => item.TenantId}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);;
        }
}
