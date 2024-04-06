// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Itineraries.EventHandlers;

    public class ItineraryDeletedEventHandler : INotificationHandler<ItineraryDeletedEvent>
    {
        private readonly ILogger<ItineraryDeletedEventHandler> _logger;

        public ItineraryDeletedEventHandler(
            ILogger<ItineraryDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ItineraryDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
