// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.Caching;
namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.AddEdit;

public class AddEditStudentCommand: ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("UID")]
    public string? UID {get;set;} 
    [Description("Last Name")]
    public string? LastName {get;set;} 
    [Description("First Name")]
    public string? FirstName {get;set;} 
    [Description("Profile Picture")]
    public string? ProfilePicture {get;set;} 
    [Description("Phone")]
    public string? Phone {get;set;} 
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Status")]
    public string? Status {get;set;} 
    [Description("School Id")]
    public int? SchoolId {get;set;} 
    [Description("Tenant Id")]
    public string? TenantId {get;set;} 


      public string CacheKey => StudentCacheKey.GetAllCacheKey;
      public CancellationTokenSource? SharedExpiryTokenSource => StudentCacheKey.SharedExpiryTokenSource();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudentDto,AddEditStudentCommand>(MemberList.None);
            CreateMap<AddEditStudentCommand, Student>(MemberList.None);
        }
    }
}

    public class AddEditStudentCommandHandler : IRequestHandler<AddEditStudentCommand, Result<int>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditStudentCommandHandler> _localizer;
        public AddEditStudentCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditStudentCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(AddEditStudentCommand request, CancellationToken cancellationToken)
        {

            if (request.Id > 0)
            {
                var item = await _context.Students.FindAsync(new object[] { request.Id }, cancellationToken) ?? throw new NotFoundException($"Student with id: [{request.Id}] not found.");
                item = _mapper.Map(request, item);
				// raise a update domain event
				item.AddDomainEvent(new StudentUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = _mapper.Map<Student>(request);
                // raise a create domain event
				item.AddDomainEvent(new StudentCreatedEvent(item));
                _context.Students.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
           
        }
    }

