// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.TransportLogs.DTOs;
using CleanArchitecture.Blazor.Application.Features.TransportLogs.Caching;

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Commands.Import;

    public class ImportTransportLogsCommand: ICacheInvalidatorRequest<Result<int>>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string CacheKey => TransportLogCacheKey.GetAllCacheKey;
        public CancellationTokenSource? SharedExpiryTokenSource => TransportLogCacheKey.SharedExpiryTokenSource();
        public ImportTransportLogsCommand(string fileName,byte[] data)
        {
           FileName = fileName;
           Data = data;
        }
    }
    public record class CreateTransportLogsTemplateCommand : IRequest<Result<byte[]>>
    {
 
    }

    public class ImportTransportLogsCommandHandler : 
                 IRequestHandler<CreateTransportLogsTemplateCommand, Result<byte[]>>,
                 IRequestHandler<ImportTransportLogsCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportTransportLogsCommandHandler> _localizer;
        private readonly IExcelService _excelService;
        private readonly TransportLogDto _dto = new();

        public ImportTransportLogsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportTransportLogsCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _excelService = excelService;
            _mapper = mapper;
        }
        #nullable disable warnings
        public async Task<Result<int>> Handle(ImportTransportLogsCommand request, CancellationToken cancellationToken)
        {
           // TODO: Implement ImportTransportLogsCommandHandler method
           var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, TransportLogDto, object?>>
            {
                // TODO: Define the fields that should be read from Excel, for example:
                { _localizer[_dto.GetMemberDescription(x=>x.StudentId)], (row, item) => item.StudentId = row[_localizer[_dto.GetMemberDescription(x=>x.StudentId)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.ItineraryId)], (row, item) => item.ItineraryId = row[_localizer[_dto.GetMemberDescription(x=>x.ItineraryId)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.DeviceId)], (row, item) => item.DeviceId = row[_localizer[_dto.GetMemberDescription(x=>x.DeviceId)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.SwipeDateTime)], (row, item) => item.SwipeDateTime = row[_localizer[_dto.GetMemberDescription(x=>x.SwipeDateTime)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Location)], (row, item) => item.Location = row[_localizer[_dto.GetMemberDescription(x=>x.Location)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Longitude)], (row, item) => item.Longitude = row[_localizer[_dto.GetMemberDescription(x=>x.Longitude)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Latitude)], (row, item) => item.Latitude = row[_localizer[_dto.GetMemberDescription(x=>x.Latitude)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.CheckType)], (row, item) => item.CheckType = row[_localizer[_dto.GetMemberDescription(x=>x.CheckType)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.UpOrDown)], (row, item) => item.UpOrDown = row[_localizer[_dto.GetMemberDescription(x=>x.UpOrDown)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.Comments)], (row, item) => item.Comments = row[_localizer[_dto.GetMemberDescription(x=>x.Comments)]].ToString() }, 
{ _localizer[_dto.GetMemberDescription(x=>x.TenantId)], (row, item) => item.TenantId = row[_localizer[_dto.GetMemberDescription(x=>x.TenantId)]].ToString() }, 

            }, _localizer[_dto.GetClassDescription()]);
            if (result.Succeeded && result.Data is not null)
            {
                foreach (var dto in result.Data)
                {
                    var exists = await _context.TransportLogs.AnyAsync(x => x.Name == dto.Name, cancellationToken);
                    if (!exists)
                    {
                        var item = _mapper.Map<TransportLog>(dto);
                        // add create domain events if this entity implement the IHasDomainEvent interface
				        // item.AddDomainEvent(new TransportLogCreatedEvent(item));
                        await _context.TransportLogs.AddAsync(item, cancellationToken);
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
        public async Task<Result<byte[]>> Handle(CreateTransportLogsTemplateCommand request, CancellationToken cancellationToken)
        {
            // TODO: Implement ImportTransportLogsCommandHandler method 
            var fields = new string[] {
                   // TODO: Define the fields that should be generate in the template, for example:
                   _localizer[_dto.GetMemberDescription(x=>x.StudentId)], 
_localizer[_dto.GetMemberDescription(x=>x.ItineraryId)], 
_localizer[_dto.GetMemberDescription(x=>x.DeviceId)], 
_localizer[_dto.GetMemberDescription(x=>x.SwipeDateTime)], 
_localizer[_dto.GetMemberDescription(x=>x.Location)], 
_localizer[_dto.GetMemberDescription(x=>x.Longitude)], 
_localizer[_dto.GetMemberDescription(x=>x.Latitude)], 
_localizer[_dto.GetMemberDescription(x=>x.CheckType)], 
_localizer[_dto.GetMemberDescription(x=>x.UpOrDown)], 
_localizer[_dto.GetMemberDescription(x=>x.Comments)], 
_localizer[_dto.GetMemberDescription(x=>x.TenantId)], 

                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
    }

