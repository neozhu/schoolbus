// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.Import;

    public class ImportStudentsCommandValidator : AbstractValidator<ImportStudentsCommand>
    {
        public ImportStudentsCommandValidator()
        {

        RuleFor(v => v.Data)
             .NotNull()
             .NotEmpty();

    }
    }

