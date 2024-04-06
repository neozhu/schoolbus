// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TransportLogs.EventHandlers;

    public class TransportLogDeletedEventHandler : INotificationHandler<TransportLogDeletedEvent>
    {
        private readonly ILogger<TransportLogDeletedEventHandler> _logger;

        public TransportLogDeletedEventHandler(
            ILogger<TransportLogDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(TransportLogDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
