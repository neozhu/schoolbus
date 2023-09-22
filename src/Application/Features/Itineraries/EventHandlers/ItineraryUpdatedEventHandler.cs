// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.EventHandlers;

    public class ItineraryUpdatedEventHandler : INotificationHandler<ItineraryUpdatedEvent>
    {
        private readonly ILogger<ItineraryUpdatedEventHandler> _logger;

        public ItineraryUpdatedEventHandler(
            ILogger<ItineraryUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ItineraryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
