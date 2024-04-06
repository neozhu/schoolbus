// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Parents.DTOs;
using CleanArchitecture.Blazor.Application.Features.Parents.Specifications;
using CleanArchitecture.Blazor.Application.Features.Parents.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Parents.Queries.Export;

public class ExportParentsQuery : ParentAdvancedFilter, IRequest<Result<byte[]>>
{
    public ParentAdvancedSpecification Specification => new ParentAdvancedSpecification(this);
}

public class ExportParentsQueryHandler :
         IRequestHandler<ExportParentsQuery, Result<byte[]>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IExcelService _excelService;
    private readonly IStringLocalizer<ExportParentsQueryHandler> _localizer;
    private readonly ParentDto _dto = new();
    public ExportParentsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IExcelService excelService,
        IStringLocalizer<ExportParentsQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _excelService = excelService;
        _localizer = localizer;
    }
#nullable disable warnings
    public async Task<Result<byte[]>> Handle(ExportParentsQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Parents.ApplySpecification(request.Specification)
                   .OrderBy($"{request.OrderBy} {request.SortDirection}")
                   .ProjectTo<ParentDto>(_mapper.ConfigurationProvider)
                   .AsNoTracking()
                   .ToListAsync(cancellationToken);
        var result = await _excelService.ExportAsync(data,
            new Dictionary<string, Func<ParentDto, object?>>()
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
        return await Result<byte[]>.SuccessAsync(result); ;
    }
}
