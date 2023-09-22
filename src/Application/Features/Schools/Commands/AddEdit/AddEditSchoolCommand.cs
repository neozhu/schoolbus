// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Schools.DTOs;
using CleanArchitecture.Blazor.Application.Features.Schools.Caching;
namespace CleanArchitecture.Blazor.Application.Features.Schools.Commands.AddEdit;

public class AddEditSchoolCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = String.Empty;
    [Description("Address")]
    public string? Address { get; set; }
    [Description("Contact")]
    public string? Contact { get; set; }
    [Description("Phone")]
    public string? Phone { get; set; }
    [Description("Tenant Id")]
    public string? TenantId { get; set; }


    public string CacheKey => SchoolCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => SchoolCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddEditSchoolCommand, School>(MemberList.None);
        }
    }
}

public class AddEditSchoolCommandHandler : IRequestHandler<AddEditSchoolCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<AddEditSchoolCommandHandler> _localizer;
    public AddEditSchoolCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<AddEditSchoolCommandHandler> localizer,
        IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(AddEditSchoolCommand request, CancellationToken cancellationToken)
    {
     
        if (request.Id > 0)
        {
            var item = await _context.Schools.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"School with id: [{request.Id}] not found.");
            item = _mapper.Map(request, item);
            // raise a update domain event
            item.AddDomainEvent(new SchoolUpdatedEvent(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var item = _mapper.Map<School>(request);
            // raise a create domain event
            item.AddDomainEvent(new SchoolCreatedEvent(item));
            _context.Schools.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }

    }
}

