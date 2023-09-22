// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Pilots.EventHandlers;

public class PilotCreatedEventHandler : INotificationHandler<PilotCreatedEvent>
{
        private readonly ILogger<PilotCreatedEventHandler> _logger;

        public PilotCreatedEventHandler(
            ILogger<PilotCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(PilotCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
