// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Parents.EventHandlers;

    public class ParentUpdatedEventHandler : INotificationHandler<ParentUpdatedEvent>
    {
        private readonly ILogger<ParentUpdatedEventHandler> _logger;

        public ParentUpdatedEventHandler(
            ILogger<ParentUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ParentUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
