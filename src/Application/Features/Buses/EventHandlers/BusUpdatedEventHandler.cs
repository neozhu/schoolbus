// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Buses.EventHandlers;

    public class BusUpdatedEventHandler : INotificationHandler<BusUpdatedEvent>
    {
        private readonly ILogger<BusUpdatedEventHandler> _logger;

        public BusUpdatedEventHandler(
            ILogger<BusUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(BusUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
