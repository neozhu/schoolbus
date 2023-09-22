// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.Import;

    public class ImportStudentsCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => StudentCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => StudentCacheKey.SharedExpiryTokenSource();
        public ImportStudentsCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateStudentsTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportStudentsCommandHandler : 
                 IRequestHandler<CreateStudentsTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportStudentsCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportStudentsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly StudentDto _dto = new();

        public ImportStudentsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportStudentsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportStudentsCommand request, CancellationToken cancellationToken)
        {

           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, StudentDto, object?>>
            {

                { _localizer[_dto.GetMemberDescription(x=>x.UID)], (row, item) => item.UID = row[_localizer[_dto.GetMemberDescription(x=>x.UID)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.LastName)], (row, item) => item.LastName = row[_localizer[_dto.GetMemberDescription(x=>x.LastName)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.FirstName)], (row, item) => item.FirstName = row[_localizer[_dto.GetMemberDescription(x=>x.FirstName)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)], (row, item) => item.ProfilePicture = row[_localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Phone)], (row, item) => item.Phone = row[_localizer[_dto.GetMemberDescription(x=>x.Phone)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Description)], (row, item) => item.Description = row[_localizer[_dto.GetMemberDescription(x=>x.Description)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Status)], (row, item) => item.Status = row[_localizer[_dto.GetMemberDescription(x=>x.Status)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.SchoolId)], (row, item) => item.SchoolId = Convert.ToInt32(row[_localizer[_dto.GetMemberDescription(x=>x.SchoolId)]]) }, 
{ _localizer[_dto.GetMemberDescription(x=>x.TenantId)], (row, item) => item.TenantId = row[_localizer[_dto.GetMemberDescription(x=>x.TenantId)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.Students.AnyAsync(x => x.SchoolId == x.SchoolId && x.UID==dto.UID, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<Student>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new StudentCreatedEvent(item));
                        await _context.Students.AddAsync(item, cancellationToken);
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
        public async Task<Result<byte[]>> Handle(CreateStudentsTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportStudentsCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.UID)], 
_localizer[_dto.GetMemberDescription(x=>x.LastName)], 
_localizer[_dto.GetMemberDescription(x=>x.FirstName)], 
_localizer[_dto.GetMemberDescription(x=>x.ProfilePicture)], 
_localizer[_dto.GetMemberDescription(x=>x.Phone)], 
_localizer[_dto.GetMemberDescription(x=>x.Description)], 
_localizer[_dto.GetMemberDescription(x=>x.Status)], 
_localizer[_dto.GetMemberDescription(x=>x.SchoolId)], 
_localizer[_dto.GetMemberDescription(x=>x.TenantId)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

