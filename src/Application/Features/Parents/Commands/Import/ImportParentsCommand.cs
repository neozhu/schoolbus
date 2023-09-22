// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Parents.DTOs;
using CleanArchitecture.Blazor.Application.Features.Parents.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Parents.Commands.Import;

    public class ImportParentsCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => ParentCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => ParentCacheKey.SharedExpiryTokenSource();
        public ImportParentsCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateParentsTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportParentsCommandHandler : 
                 IRequestHandler<CreateParentsTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportParentsCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportParentsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly ParentDto _dto = new();

        public ImportParentsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportParentsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportParentsCommand request, CancellationToken cancellationToken)
        {
           // TODO: Implement ImportParentsCommandHandler method
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, ParentDto, object?>>
            {
                // TODO: Define the fields that should be read from Excel, for example:
                { _localizer[_dto.GetMemberDescription(x=>x.LastName)], (row, item) => item.LastName = row[_localizer[_dto.GetMemberDescription(x=>x.LastName)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.FirstName)], (row, item) => item.FirstName = row[_localizer[_dto.GetMemberDescription(x=>x.FirstName)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)], (row, item) => item.ProfilePicture = row[_localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Phone)], (row, item) => item.Phone = row[_localizer[_dto.GetMemberDescription(x=>x.Phone)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Description)], (row, item) => item.Description = row[_localizer[_dto.GetMemberDescription(x=>x.Description)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Status)], (row, item) => item.Status = row[_localizer[_dto.GetMemberDescription(x=>x.Status)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.TenantId)], (row, item) => item.TenantId = row[_localizer[_dto.GetMemberDescription(x=>x.TenantId)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.Parents.AnyAsync(x => x.LastName == dto.LastName && x.FirstName==dto.FirstName, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<Parent>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new ParentCreatedEvent(item));
                        await _context.Parents.AddAsync(item, cancellationToken);
                    }
                 }
                 await _context.SaveChangesAsync(cancellationToken);
                 return await Result<int>.SuccessAsync(result.Data.Count());
           }
           else
           {
               return await Result<int>.FailureAsync(result.Errors);
           }
        }
        public async Task<Result<byte[]>> Handle(CreateParentsTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportParentsCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.LastName)], 
_localizer[_dto.GetMemberDescription(x=>x.FirstName)], 
_localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)], 
_localizer[_dto.GetMemberDescription(x=>x.Phone)], 
_localizer[_dto.GetMemberDescription(x=>x.Description)], 
_localizer[_dto.GetMemberDescription(x=>x.Status)], 
_localizer[_dto.GetMemberDescription(x=>x.TenantId)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

