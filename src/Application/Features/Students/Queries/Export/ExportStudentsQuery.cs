// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.Specifications;
using CleanArchitecture.Blazor.Application.Features.Students.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Students.Queries.Export;

public class ExportStudentsQuery : StudentAdvancedFilter, IRequest<Result<byte[]>>
{
      public StudentAdvancedSpecification Specification => new StudentAdvancedSpecification(this);
}
    
public class ExportStudentsQueryHandler :
         IRequestHandler<ExportStudentsQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportStudentsQueryHandler> _localizer;
        private readonly StudentDto _dto = new();
        public ExportStudentsQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportStudentsQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportStudentsQuery request, CancellationToken cancellationToken)
        {

            var data = await _context.Students.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<StudentDto, object?>>()
                {
                    {_localizer[_dto.GetMemberDescription(x=>x.UID)],item => item.UID}, 
{_localizer[_dto.GetMemberDescription(x=>x.LastName)],item => item.LastName}, 
{_localizer[_dto.GetMemberDescription(x=>x.FirstName)],item => item.FirstName}, 
{_localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)],item => item.ProfilePicture}, 
{_localizer[_dto.GetMemberDescription(x=>x.Phone)],item => item.Phone}, 
{_localizer[_dto.GetMemberDescription(x=>x.Description)],item => item.Description}, 
{_localizer[_dto.GetMemberDescription(x=>x.Status)],item => item.Status}, 
{_localizer[_dto.GetMemberDescription(x=>x.SchoolId)],item => item.SchoolId}, 
{_localizer[_dto.GetMemberDescription(x=>x.TenantId)],item => item.TenantId}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);;
        }
}
