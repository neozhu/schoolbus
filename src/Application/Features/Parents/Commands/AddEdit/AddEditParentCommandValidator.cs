// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Parents.Commands.AddEdit;

public class AddEditParentCommandValidator : AbstractValidator<AddEditParentCommand>
{
    public AddEditParentCommandValidator()
    {

        RuleFor(v => v.LastName)
             .MaximumLength(256)
             .NotEmpty();
        RuleFor(v => v.Phone)
             .MaximumLength(256)
             .NotEmpty();
    }
     public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
     {
        var result = await ValidateAsync(ValidationContext<AddEditParentCommand>.CreateWithOptions((AddEditParentCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
     };
}

