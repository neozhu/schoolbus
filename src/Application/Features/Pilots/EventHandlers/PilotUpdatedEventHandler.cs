// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Pilots.EventHandlers;

    public class PilotUpdatedEventHandler : INotificationHandler<PilotUpdatedEvent>
    {
        private readonly ILogger<PilotUpdatedEventHandler> _logger;

        public PilotUpdatedEventHandler(
            ILogger<PilotUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(PilotUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
