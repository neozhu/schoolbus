// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CleanArchitecture.Blazor.Application.Features.Itineraries.DTOs;
using CleanArchitecture.Blazor.Application.Features.Students.Caching;
using CleanArchitecture.Blazor.Application.Features.Students.DTOs;

namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.AssignItinerary;

public class AssignItineraryCommand : ICacheInvalidatorRequest<Result<int>>
{
    public List<StudentDto> Students { get; set; }
    public ItineraryDto? Itinerary { get; set; }
    public string TenantId { get; set; }
    public string CacheKey => StudentCacheKey.GetAllCacheKey;
    public CancellationTokenSource? SharedExpiryTokenSource => StudentCacheKey.SharedExpiryTokenSource();

}

public class AssignItineraryCommandHandler :
             IRequestHandler<AssignItineraryCommand, Result<int>>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<AssignItineraryCommandHandler> _localizer;
    public AssignItineraryCommandHandler(
        IApplicationDbContext context,
        IStringLocalizer<AssignItineraryCommandHandler> localizer,
         IMapper mapper
        )
    {
        _context = context;
        _localizer = localizer;
        _mapper = mapper;
    }
    public async Task<Result<int>> Handle(AssignItineraryCommand request, CancellationToken cancellationToken)
    {
        foreach(var item in request.Students)
        {
            var student = await _context.Students.FindAsync(item.Id)??throw new NotFoundException($"{item.Id} not found.");
            student.ItineraryId = request.Itinerary!.Id;
        }
        var result = await _context.SaveChangesAsync(cancellationToken);
        return await Result<int>.SuccessAsync(result);
    }

}

