// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.TripReports.EventHandlers;

    public class TripReportDeletedEventHandler : INotificationHandler<TripReportDeletedEvent>
    {
        private readonly ILogger<TripReportDeletedEventHandler> _logger;

        public TripReportDeletedEventHandler(
            ILogger<TripReportDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(TripReportDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
