// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Schools.EventHandlers;

    public class SchoolDeletedEventHandler : INotificationHandler<SchoolDeletedEvent>
    {
        private readonly ILogger<SchoolDeletedEventHandler> _logger;

        public SchoolDeletedEventHandler(
            ILogger<SchoolDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(SchoolDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
