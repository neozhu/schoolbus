// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.EventHandlers;

    public class TransportLogUpdatedEventHandler : INotificationHandler<TransportLogUpdatedEvent>
    {
        private readonly ILogger<TransportLogUpdatedEventHandler> _logger;

        public TransportLogUpdatedEventHandler(
            ILogger<TransportLogUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(TransportLogUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
