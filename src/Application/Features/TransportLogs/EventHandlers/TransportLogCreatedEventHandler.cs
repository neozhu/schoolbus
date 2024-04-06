// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.EventHandlers;

public class TransportLogCreatedEventHandler : INotificationHandler<TransportLogCreatedEvent>
{
        private readonly ILogger<TransportLogCreatedEventHandler> _logger;

        public TransportLogCreatedEventHandler(
            ILogger<TransportLogCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(TransportLogCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
