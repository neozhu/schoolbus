// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Parents.EventHandlers;

public class ParentCreatedEventHandler : INotificationHandler<ParentCreatedEvent>
{
        private readonly ILogger<ParentCreatedEventHandler> _logger;

        public ParentCreatedEventHandler(
            ILogger<ParentCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ParentCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
