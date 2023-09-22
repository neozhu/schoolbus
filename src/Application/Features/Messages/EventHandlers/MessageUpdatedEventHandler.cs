// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Messages.EventHandlers;

    public class MessageUpdatedEventHandler : INotificationHandler<MessageUpdatedEvent>
    {
        private readonly ILogger<MessageUpdatedEventHandler> _logger;

        public MessageUpdatedEventHandler(
            ILogger<MessageUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(MessageUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
