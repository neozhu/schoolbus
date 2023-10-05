// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.AddEdit;

public class AddEditTripReportCommandValidator : AbstractValidator<AddEditTripReportCommand>
{
    public AddEditTripReportCommandValidator()
    {
        RuleFor(v => v.ItineraryId).NotNull();
        RuleFor(v => v.PilotId).NotNull();
        RuleFor(v => v.PlatNumber).NotEmpty();
        RuleFor(v => v.DepartureDate).NotNull();
    }
     public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
     {
        var result = await ValidateAsync(ValidationContext<AddEditTripReportCommand>.CreateWithOptions((AddEditTripReportCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
     };
}

