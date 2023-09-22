// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Buses.DTOs;
using CleanArchitecture.Blazor.Application.Features.Buses.Caching;

namespace CleanArchitecture.Blazor.Application.Features.Buses.Commands.Import;

    public class ImportBusesCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => BusCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => BusCacheKey.SharedExpiryTokenSource();
        public ImportBusesCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateBusesTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportBusesCommandHandler : 
                 IRequestHandler<CreateBusesTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportBusesCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportBusesCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly BusDto _dto = new();

        public ImportBusesCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportBusesCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportBusesCommand request, CancellationToken cancellationToken)
        {
           // TODO: Implement ImportBusesCommandHandler method
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, BusDto, object?>>
            {
                // TODO: Define the fields that should be read from Excel, for example:
                { _localizer[_dto.GetMemberDescription(x=>x.PlatNumber)], (row, item) => item.PlatNumber = row[_localizer[_dto.GetMemberDescription(x=>x.PlatNumber)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.DeviceId)], (row, item) => item.DeviceId = row[_localizer[_dto.GetMemberDescription(x=>x.DeviceId)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Status)], (row, item) => item.Status = row[_localizer[_dto.GetMemberDescription(x=>x.Status)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Description)], (row, item) => item.Description = row[_localizer[_dto.GetMemberDescription(x=>x.Description)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.TenantId)], (row, item) => item.TenantId = row[_localizer[_dto.GetMemberDescription(x=>x.TenantId)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.Buses.AnyAsync(x => x.PlatNumber == dto.PlatNumber, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<Bus>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new BusCreatedEvent(item));
                        await _context.Buses.AddAsync(item, cancellationToken);
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
        public async Task<Result<byte[]>> Handle(CreateBusesTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportBusesCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.PlatNumber)], 
_localizer[_dto.GetMemberDescription(x=>x.DeviceId)], 
_localizer[_dto.GetMemberDescription(x=>x.Status)], 
_localizer[_dto.GetMemberDescription(x=>x.Description)], 
_localizer[_dto.GetMemberDescription(x=>x.TenantId)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

