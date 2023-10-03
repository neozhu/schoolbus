// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.AssignItinerary;

public class AssignItineraryCommandValidator : AbstractValidator<AssignItineraryCommand>
{
    public AssignItineraryCommandValidator()
    {
        RuleFor(v => v.TenantId).NotEmpty();
        RuleFor(v => v.Itinerary).NotNull();
        RuleFor(v => v.Students).NotNull().NotEmpty();

    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AssignItineraryCommand>.CreateWithOptions((AssignItineraryCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}


