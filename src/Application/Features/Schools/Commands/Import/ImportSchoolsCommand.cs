// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Schools.Commands.Import;

    public class ImportSchoolsCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => SchoolCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => SchoolCacheKey.SharedExpiryTokenSource();
        public ImportSchoolsCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateSchoolsTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportSchoolsCommandHandler : 
                 IRequestHandler<CreateSchoolsTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportSchoolsCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportSchoolsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly SchoolDto _dto = new();

        public ImportSchoolsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportSchoolsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportSchoolsCommand request, CancellationToken cancellationToken)
        {
           // TODO: Implement ImportSchoolsCommandHandler method
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, SchoolDto, object?>>
            {
                // TODO: Define the fields that should be read from Excel, for example:
                { _localizer[_dto.GetMemberDescription(x=>x.Name)], (row, item) => item.Name = row[_localizer[_dto.GetMemberDescription(x=>x.Name)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Address)], (row, item) => item.Address = row[_localizer[_dto.GetMemberDescription(x=>x.Address)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Contact)], (row, item) => item.Contact = row[_localizer[_dto.GetMemberDescription(x=>x.Contact)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Phone)], (row, item) => item.Phone = row[_localizer[_dto.GetMemberDescription(x=>x.Phone)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.TenantId)], (row, item) => item.TenantId = row[_localizer[_dto.GetMemberDescription(x=>x.TenantId)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.Schools.AnyAsync(x => x.Name == dto.Name, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<School>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new SchoolCreatedEvent(item));
                        await _context.Schools.AddAsync(item, cancellationToken);
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
        public async Task<Result<byte[]>> Handle(CreateSchoolsTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportSchoolsCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.Name)], 
_localizer[_dto.GetMemberDescription(x=>x.Address)], 
_localizer[_dto.GetMemberDescription(x=>x.Contact)], 
_localizer[_dto.GetMemberDescription(x=>x.Phone)], 
_localizer[_dto.GetMemberDescription(x=>x.TenantId)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

