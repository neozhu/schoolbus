// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Buses.Commands.Import;

    public class ImportBusesCommandValidator : AbstractValidator<ImportBusesCommand>
    {
        public ImportBusesCommandValidator()
        {

        RuleFor(v => v.Data)             .NotNull()             .NotEmpty();

    }
    }

