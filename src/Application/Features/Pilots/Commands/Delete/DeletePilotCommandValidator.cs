// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Pilots.Commands.Delete;

public class DeletePilotCommandValidator : AbstractValidator<DeletePilotCommand>
{
        public DeletePilotCommandValidator()
        {
           
           RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));

        }
}
    

