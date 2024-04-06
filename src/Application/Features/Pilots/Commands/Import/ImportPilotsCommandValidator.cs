// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Pilots.Commands.Import;

public class ImportPilotsCommandValidator : AbstractValidator<ImportPilotsCommand>
{
    public ImportPilotsCommandValidator()
    {

        RuleFor(v => v.Data)
             .NotNull()
             .NotEmpty();

    }
}

