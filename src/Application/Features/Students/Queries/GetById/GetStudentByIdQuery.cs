// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.Caching;
using CleanArchitecture.Blazor.Application.Features.Students.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Students.Queries.GetById;

public class GetStudentByIdQuery : ICacheableRequest<StudentDto>
{
    public int Id { get; }
    public string CacheKey => StudentCacheKey.GetByIdCacheKey($"{Id}");
    public MemoryCacheEntryOptions? Options => StudentCacheKey.MemoryCacheEntryOptions;
    public GetStudentByIdQuery(int id)
    {
        Id = id;
    }
}
public class GetStudentByUIdQuery : ICacheableRequest<Result<StudentDto>>
{
    public string UId { get; }
    public string TenantId { get; }
    public int? TripId { get; set; }
    public string CacheKey => StudentCacheKey.GetByIdCacheKey($"{UId},{TripId}");
    public MemoryCacheEntryOptions? Options => StudentCacheKey.MemoryCacheEntryOptions;
    public GetStudentByUIdQuery(string uid, string tenantId)
    {
        UId = uid;
        TenantId = tenantId;
    }
}
public class GetStudentBySchoolIdQuery : ICacheableRequest<List<StudentDto>>
{
    public required int SchoolId { get; set; }
    public string? Name { get; set; }
    public string CacheKey => StudentCacheKey.GetBySchoolIdCacheKey($"{SchoolId}");
    public MemoryCacheEntryOptions? Options => StudentCacheKey.MemoryCacheEntryOptions;
}
public class GetStudentByIdQueryHandler :
     IRequestHandler<GetStudentBySchoolIdQuery, List<StudentDto>>,
     IRequestHandler<GetStudentByIdQuery, StudentDto>,
    IRequestHandler<GetStudentByUIdQuery, Result<StudentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetStudentByIdQueryHandler> _localizer;

    public GetStudentByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IStringLocalizer<GetStudentByIdQueryHandler> localizer
        )
    {
        _context = context;
        _mapper = mapper;
        _localizer = localizer;
    }
    public async Task<Result<StudentDto>> Handle(GetStudentByUIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Students.ApplySpecification(new StudentByUIDSpecification(request.UId, request.TenantId))
                     .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                     .FirstOrDefaultAsync(cancellationToken);
        if (data is null)
        {
            return await Result<StudentDto>.FailureAsync(new[] { $"Invalid QR code：{request.UId}" });
        }
        else
        {
            if(request.TripId is not null)
            {
                var geton = await _context.TripLogs.Where(x => x.TripId == request.TripId && x.StudentId == data.Id && x.GetOffDateTime2 == null).FirstOrDefaultAsync(cancellationToken);
                if(geton is null)
                {
                    data.OnOff = "On";
                }
                else
                {
                   var diff = geton.GetOnDateTime - DateTime.Now;
                    if (diff.Value.TotalSeconds <= -15)
                    {
                        data.OnOff = "Off";
                    }
                    else
                    {
                        data.OnOff = "On";
                    }
                }
                 
            }
            

            return await Result<StudentDto>.SuccessAsync(data);
        }

    }
    public async Task<StudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {

        var data = await _context.Students.ApplySpecification(new StudentByIdSpecification(request.Id))
                     .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                     .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Student with id: [{request.Id}] not found.");
        return data;
    }
    public async Task<List<StudentDto>> Handle(GetStudentBySchoolIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Students.ApplySpecification(new StudentBySchoolIdSpecification(request.SchoolId, request.Name))
                     .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                     .ToListAsync(cancellationToken);
        return data;
    }
}
