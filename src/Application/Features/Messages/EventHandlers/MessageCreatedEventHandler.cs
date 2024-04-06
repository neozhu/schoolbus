// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Messages.EventHandlers;

public class MessageCreatedEventHandler : INotificationHandler<MessageCreatedEvent>
{
        private readonly ILogger<MessageCreatedEventHandler> _logger;

        public MessageCreatedEventHandler(
            ILogger<MessageCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(MessageCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
}
