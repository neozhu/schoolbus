// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Students.EventHandlers;

    public class StudentDeletedEventHandler : INotificationHandler<StudentDeletedEvent>
    {
        private readonly ILogger<StudentDeletedEventHandler> _logger;

        public StudentDeletedEventHandler(
            ILogger<StudentDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(StudentDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
