// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.EventHandlers;

public class ItineraryCreatedEventHandler : INotificationHandler<ItineraryCreatedEvent>
{
        private readonly ILogger<ItineraryCreatedEventHandler> _logger;

        public ItineraryCreatedEventHandler(
            ILogger<ItineraryCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ItineraryCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
