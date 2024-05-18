// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.Commands.AddEdit;

public class AddEditItineraryCommandValidator : AbstractValidator<AddEditItineraryCommand>
{
    public AddEditItineraryCommandValidator()
    {
  
        RuleFor(v => v.Name)
             .MaximumLength(256)
             .NotEmpty();
        RuleFor(v => v.StartingStation)
            .MaximumLength(256)
            .NotEmpty();
        RuleFor(v => v.TerminalStation)
            .MaximumLength(256)
            .NotEmpty();
        RuleFor(v => v.SchoolId).NotNull();
        RuleFor(v => v.DriverId).NotNull();
        RuleFor(v => v.BusId).NotNull();
        RuleFor(v => v.FirstTime).NotEmpty();
        RuleFor(v => v.LastTime).NotEmpty();
    }
     public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
     {
        var result = await ValidateAsync(ValidationContext<AddEditItineraryCommand>.CreateWithOptions((AddEditItineraryCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
     };
}

