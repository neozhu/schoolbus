// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Messages.Commands.Update;

public class UpdateMessageCommandValidator : AbstractValidator<UpdateMessageCommand>
{
        public UpdateMessageCommandValidator()
        {
           RuleFor(v => v.Id).NotNull();
        RuleFor(v => v.From)
            .MaximumLength(256)
            .NotEmpty();
        RuleFor(v => v.To)
                .MaximumLength(256)
                .NotEmpty();

    }
     public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
     {
        var result = await ValidateAsync(ValidationContext<UpdateMessageCommand>.CreateWithOptions((UpdateMessageCommand)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
     };
}

