﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.AddEdit;

public class AddEditStudentCommandValidator : AbstractValidator<AddEditStudentCommand>
{
    public AddEditStudentCommandValidator()
    {

        RuleFor(v => v.UID)
             .MaximumLength(256)
             .NotEmpty();
        RuleFor(v => v.LastName)
             .MaximumLength(256)
             .NotEmpty();
        RuleFor(v => v.SchoolId).NotNull()
             .GreaterThan(0);

    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<AddEditStudentCommand>.CreateWithOptions((AddEditStudentCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

