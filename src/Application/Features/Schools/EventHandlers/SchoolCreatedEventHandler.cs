// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Schools.EventHandlers;

public class SchoolCreatedEventHandler : INotificationHandler<SchoolCreatedEvent>
{
        private readonly ILogger<SchoolCreatedEventHandler> _logger;

        public SchoolCreatedEventHandler(
            ILogger<SchoolCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(SchoolCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
