// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Parents.EventHandlers;

    public class ParentDeletedEventHandler : INotificationHandler<ParentDeletedEvent>
    {
        private readonly ILogger<ParentDeletedEventHandler> _logger;

        public ParentDeletedEventHandler(
            ILogger<ParentDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ParentDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
