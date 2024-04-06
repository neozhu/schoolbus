// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Specifications;
using CleanArchitecture.Blazor.Application.Features.Itineraries.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Queries.Export;

public class ExportItinerariesQuery : ItineraryAdvancedFilter, IRequest<Result<byte[]>>
{
      public ItineraryAdvancedSpecification Specification => new ItineraryAdvancedSpecification(this);
}
    
public class ExportItinerariesQueryHandler :
         IRequestHandler<ExportItinerariesQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportItinerariesQueryHandler> _localizer;
        private readonly ItineraryDto _dto = new();
        public ExportItinerariesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportItinerariesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportItinerariesQuery request, CancellationToken cancellationToken)
        {

            var data = await _context.Itineraries.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<ItineraryDto, object?>>()
                {

                    {_localizer[_dto.GetMemberDescription(x=>x.Name)],item => item.Name}, 
{_localizer[_dto.GetMemberDescription(x=>x.Description)],item => item.Description}, 
{_localizer[_dto.GetMemberDescription(x=>x.BusId)],item => item.BusId}, 
{_localizer[_dto.GetMemberDescription(x=>x.PilotId)],item => item.PilotId}, 
{_localizer[_dto.GetMemberDescription(x=>x.SchoolId)],item => item.SchoolId}, 
{_localizer[_dto.GetMemberDescription(x=>x.FirstTime)],item => item.FirstTime}, 
{_localizer[_dto.GetMemberDescription(x=>x.LastTime)],item => item.LastTime}, 
{_localizer[_dto.GetMemberDescription(x=>x.StartingStation)],item => item.StartingStation}, 
{_localizer[_dto.GetMemberDescription(x=>x.TerminalStation)],item => item.TerminalStation}, 
{_localizer[_dto.GetMemberDescription(x=>x.Disabled)],item => item.Disabled}, 
{_localizer[_dto.GetMemberDescription(x=>x.TenantId)],item => item.TenantId}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);;
        }
}
