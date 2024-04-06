// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Messages.DTOs;
using CleanArchitecture.Blazor.Application.Features.Messages.Specifications;
using CleanArchitecture.Blazor.Application.Features.Messages.Queries.Pagination;

namespace CleanArchitecture.Blazor.Application.Features.Messages.Queries.Export;

public class ExportMessagesQuery : MessageAdvancedFilter, IRequest<Result<byte[]>>
{
      public MessageAdvancedSpecification Specification => new MessageAdvancedSpecification(this);
}
    
public class ExportMessagesQueryHandler :
         IRequestHandler<ExportMessagesQuery, Result<byte[]>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportMessagesQueryHandler> _localizer;
        private readonly MessageDto _dto = new();
        public ExportMessagesQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ExportMessagesQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportMessagesQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Messages.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<MessageDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                    {_localizer[_dto.GetMemberDescription(x=>x.From)],item => item.From}, 
{_localizer[_dto.GetMemberDescription(x=>x.To)],item => item.To}, 
{_localizer[_dto.GetMemberDescription(x=>x.Phone)],item => item.Phone}, 
{_localizer[_dto.GetMemberDescription(x=>x.Content)],item => item.Content}, 
{_localizer[_dto.GetMemberDescription(x=>x.SentTime)],item => item.SentTime}, 
{_localizer[_dto.GetMemberDescription(x=>x.ReadTime)],item => item.ReadTime}, 
{_localizer[_dto.GetMemberDescription(x=>x.Status)],item => item.Status}, 
{_localizer[_dto.GetMemberDescription(x=>x.TenantId)],item => item.TenantId}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);;
        }
}
