// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Students.Commands.Delete;

public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
        public DeleteStudentCommandValidator()
        {
          
           RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));

        }
}
    

