﻿using Blef.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blef.Shared.Infrastructure.Events;

internal class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ILogger<DomainEventDispatcher> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellation)
        where TEvent : IDomainEvent
    {
        try
        {
            var handler = _serviceProvider.GetRequiredService<IDomainEventHandler<TEvent>>();
            await handler.Handle(@event, cancellation);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: "{@Event}", @event);
            throw;
        }
    }

    public async Task Dispatch<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellation)
        where TEvent : IDomainEvent
    {
        foreach (var domainEvent in events)
            await Dispatch(domainEvent, cancellation);
    }
}