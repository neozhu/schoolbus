// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TripReports.EventHandlers;

    public class TripReportUpdatedEventHandler : INotificationHandler<TripReportUpdatedEvent>
    {
        private readonly ILogger<TripReportUpdatedEventHandler> _logger;

        public TripReportUpdatedEventHandler(
            ILogger<TripReportUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(TripReportUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
