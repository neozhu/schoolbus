// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.Commands.Import;

    public class ImportTransportLogsCommandValidator : AbstractValidator<ImportTransportLogsCommand>
    {
        public ImportTransportLogsCommandValidator()
        {

        RuleFor(v => v.Data)
             .NotNull()
             .NotEmpty();

    }
    }

