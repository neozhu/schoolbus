// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Buses.Commands.AddEdit;

public class AddEditBusCommandValidator : AbstractValidator<AddEditBusCommand>
{
    public AddEditBusCommandValidator()
    {

        RuleFor(v => v.PlatNumber).MaximumLength(256).NotEmpty();

    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AddEditBusCommand>.CreateWithOptions((AddEditBusCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

