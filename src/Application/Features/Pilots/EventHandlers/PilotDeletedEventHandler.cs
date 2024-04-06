// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Pilots.EventHandlers;

    public class PilotDeletedEventHandler : INotificationHandler<PilotDeletedEvent>
    {
        private readonly ILogger<PilotDeletedEventHandler> _logger;

        public PilotDeletedEventHandler(
            ILogger<PilotDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(PilotDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
