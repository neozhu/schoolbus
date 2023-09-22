// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Commands.AddEdit;

public class AddEditTransportLogCommandValidator : AbstractValidator<AddEditTransportLogCommand>
{
    public AddEditTransportLogCommandValidator()
    {

        RuleFor(v => v.ItineraryId).GreaterThan(0);
        RuleFor(v => v.StudentId).GreaterThan(0);
        RuleFor(v => v.SwipeDateTime).NotNull();

    }
     public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
     {
        var result = await ValidateAsync(ValidationContext<AddEditTransportLogCommand>.CreateWithOptions((AddEditTransportLogCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
     };
}

