// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Messages.Commands.Delete;

public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
{
        public DeleteMessageCommandValidator()
        {
          
            RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));
          
        }
}
    

