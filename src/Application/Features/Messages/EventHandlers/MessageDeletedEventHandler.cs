// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Messages.EventHandlers;

    public class MessageDeletedEventHandler : INotificationHandler<MessageDeletedEvent>
    {
        private readonly ILogger<MessageDeletedEventHandler> _logger;

        public MessageDeletedEventHandler(
            ILogger<MessageDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(MessageDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
