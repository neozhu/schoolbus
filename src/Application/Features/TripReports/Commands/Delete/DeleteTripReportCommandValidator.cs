// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TripReports.Commands.Delete;

public class DeleteTripReportCommandValidator : AbstractValidator<DeleteTripReportCommand>
{
        public DeleteTripReportCommandValidator()
        {
          
            RuleFor(v => v.Id).NotNull().ForEach(v=>v.GreaterThan(0));
          
        }
}
    

