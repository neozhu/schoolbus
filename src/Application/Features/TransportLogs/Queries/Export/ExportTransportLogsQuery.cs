// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Specifications;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Queries.Export;

public class ExportTransportLogsQuery : TransportLogAdvancedFilter, IRequest<Result<byte[]>>
{

      public TransportLogAdvancedSpecification Specification => new TransportLogAdvancedSpecification(this);
}
    
public class ExportTransportLogsQueryHandler :
         IRequestHandler<ExportTransportLogsQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportTransportLogsQueryHandler> _localizer;
        private readonly TransportLogDto _dto = new();
        public ExportTransportLogsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportTransportLogsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportTransportLogsQuery request, CancellationToken cancellationToken)
        {
  
            var data = await _context.TransportLogs.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<TransportLogDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<TransportLogDto, object?>>()
                {

                    {_localizer[_dto.GetMemberDescription(x=>x.StudentId)],item => item.StudentId}, 
{_localizer[_dto.GetMemberDescription(x=>x.ItineraryId)],item => item.ItineraryId}, 
{_localizer[_dto.GetMemberDescription(x=>x.DeviceId)],item => item.DeviceId}, 
{_localizer[_dto.GetMemberDescription(x=>x.SwipeDateTime)],item => item.SwipeDateTime}, 
{_localizer[_dto.GetMemberDescription(x=>x.Location)],item => item.Location}, 
{_localizer[_dto.GetMemberDescription(x=>x.Longitude)],item => item.Longitude}, 
{_localizer[_dto.GetMemberDescription(x=>x.Latitude)],item => item.Latitude}, 
{_localizer[_dto.GetMemberDescription(x=>x.CheckType)],item => item.CheckType}, 
{_localizer[_dto.GetMemberDescription(x=>x.UpOrDown)],item => item.UpOrDown}, 
{_localizer[_dto.GetMemberDescription(x=>x.Comments)],item => item.Comments}, 
{_localizer[_dto.GetMemberDescription(x=>x.TenantId)],item => item.TenantId}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);;
        }
}
