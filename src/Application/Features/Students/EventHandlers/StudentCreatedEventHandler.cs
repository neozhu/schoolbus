// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Students.EventHandlers;

public class StudentCreatedEventHandler : INotificationHandler<StudentCreatedEvent>
{
        private readonly ILogger<StudentCreatedEventHandler> _logger;

        public StudentCreatedEventHandler(
            ILogger<StudentCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(StudentCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
