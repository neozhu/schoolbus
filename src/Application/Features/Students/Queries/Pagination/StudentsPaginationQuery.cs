// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Students.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.Caching;
using CleanArchitecture.Blazor.Application.Features.Students.Specifications;

namespace CleanArchitecture.Blazor.Application.Features.Students.Queries.Pagination;

public class StudentsWithPaginationQuery : StudentAdvancedFilter, ICacheableRequest<PaginatedData<StudentDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => StudentCacheKey.GetPaginationCacheKey($"{this}");
    public MemoryCacheEntryOptions? Options => StudentCacheKey.MemoryCacheEntryOptions;
    public StudentAdvancedSpecification Specification => new StudentAdvancedSpecification(this);
}
    
public class StudentsWithPaginationQueryHandler :
         IRequestHandler<StudentsWithPaginationQuery, PaginatedData<StudentDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<StudentsWithPaginationQueryHandler> _localizer;

        public StudentsWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IStringLocalizer<StudentsWithPaginationQueryHandler> localizer
            )
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedData<StudentDto>> Handle(StudentsWithPaginationQuery request, CancellationToken cancellationToken)
        {
       
           var data = await _context.Students.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                    .ProjectToPaginatedDataAsync<Student, StudentDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}