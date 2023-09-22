// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.Specifications;
using CleanArchitecture.Blazor.Application.Features.Pilots.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Pilots.Queries.Export;

public class ExportPilotsQuery : PilotAdvancedFilter, IRequest<Result<byte[]>>
{
      public PilotAdvancedSpecification Specification => new PilotAdvancedSpecification(this);
}
    
public class ExportPilotsQueryHandler :
         IRequestHandler<ExportPilotsQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportPilotsQueryHandler> _localizer;
        private readonly PilotDto _dto = new();
        public ExportPilotsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportPilotsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportPilotsQuery request, CancellationToken cancellationToken)
        {
            // TODO: Implement ExportPilotsQueryHandler method 
            var data = await _context.Pilots.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<PilotDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<PilotDto, object?>>()
                {

                    {_localizer[_dto.GetMemberDescription(x=>x.LastName)],item => item.LastName}, 
{_localizer[_dto.GetMemberDescription(x=>x.FirstName)],item => item.FirstName}, 
{_localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)],item => item.ProfilePicture}, 
{_localizer[_dto.GetMemberDescription(x=>x.Phone)],item => item.Phone}, 
{_localizer[_dto.GetMemberDescription(x=>x.Description)],item => item.Description}, 
{_localizer[_dto.GetMemberDescription(x=>x.Status)],item => item.Status}, 
{_localizer[_dto.GetMemberDescription(x=>x.TenantId)],item => item.TenantId}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);;
        }
}
