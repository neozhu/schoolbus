// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Schools.Queries.Export;

public class ExportSchoolsQuery : SchoolAdvancedFilter, IRequest<Result<byte[]>>
{
    public SchoolAdvancedSpecification Specification => new SchoolAdvancedSpecification(this);
}

public class ExportSchoolsQueryHandler :
         IRequestHandler<ExportSchoolsQuery, Result<byte[]>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IExcelService _excelService;
    private readonly IStringLocalizer<ExportSchoolsQueryHandler> _localizer;
    private readonly SchoolDto _dto = new();
    public ExportSchoolsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IExcelService excelService,
        IStringLocalizer<ExportSchoolsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _excelService = excelService;
        _localizer = localizer;
    }
#nullable disable warnings
    public async Task<Result<byte[]>> Handle(ExportSchoolsQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Schools.ApplySpecification(request.Specification)
                   .OrderBy($"{request.OrderBy} {request.SortDirection}")
                   .ProjectTo<SchoolDto>(_mapper.ConfigurationProvider)
                   .AsNoTracking()
                   .ToListAsync(cancellationToken);
        var result = await _excelService.ExportAsync(data,
            new Dictionary<string, Func<SchoolDto, object?>>()
            {
                    {_localizer[_dto.GetMemberDescription(x=>x.Name)],item => item.Name},
{_localizer[_dto.GetMemberDescription(x=>x.Address)],item => item.Address},
{_localizer[_dto.GetMemberDescription(x=>x.Contact)],item => item.Contact},
{_localizer[_dto.GetMemberDescription(x=>x.Phone)],item => item.Phone},
{_localizer[_dto.GetMemberDescription(x=>x.TenantId)],item => item.TenantId},

            }
            , _localizer[_dto.GetClassDescription()]);
        return await Result<byte[]>.SuccessAsync(result); ;
    }
}
