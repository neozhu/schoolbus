// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TripReports.EventHandlers;

public class TripReportCreatedEventHandler : INotificationHandler<TripReportCreatedEvent>
{
        private readonly ILogger<TripReportCreatedEventHandler> _logger;

        public TripReportCreatedEventHandler(
            ILogger<TripReportCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(TripReportCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
