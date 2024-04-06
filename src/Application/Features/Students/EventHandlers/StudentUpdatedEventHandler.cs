// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Students.EventHandlers;

    public class StudentUpdatedEventHandler : INotificationHandler<StudentUpdatedEvent>
    {
        private readonly ILogger<StudentUpdatedEventHandler> _logger;

        public StudentUpdatedEventHandler(
            ILogger<StudentUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(StudentUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
