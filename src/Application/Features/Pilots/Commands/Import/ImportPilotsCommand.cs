// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Pilots.DTOs;
using CleanArchitecture.Blazor.Application.Features.Pilots.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Pilots.Commands.Import;

    public class ImportPilotsCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => PilotCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => PilotCacheKey.SharedExpiryTokenSource();
        public ImportPilotsCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreatePilotsTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportPilotsCommandHandler : 
                 IRequestHandler<CreatePilotsTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportPilotsCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportPilotsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly PilotDto _dto = new();

        public ImportPilotsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportPilotsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportPilotsCommand request, CancellationToken cancellationToken)
        {
      
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, PilotDto, object?>>
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
                    var exists = await _context.Pilots.AnyAsync(x => x.LastName == dto.LastName && x.Phone==dto.LastName, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<Pilot>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new PilotCreatedEvent(item));
                        await _context.Pilots.AddAsync(item, cancellationToken);
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
        public async Task<Result<byte[]>> Handle(CreatePilotsTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportPilotsCommandHandler method 
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

