﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Blazor.Application.Features.Schools.EventHandlers;

    public class SchoolUpdatedEventHandler : INotificationHandler<SchoolUpdatedEvent>
    {
        private readonly ILogger<SchoolUpdatedEventHandler> _logger;

        public SchoolUpdatedEventHandler(
            ILogger<SchoolUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(SchoolUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().FullName);
            return Task.CompletedTask;
        }
    }
