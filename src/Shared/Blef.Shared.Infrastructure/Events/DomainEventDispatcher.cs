using Blef.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blef.Shared.Infrastructure.Events;

internal sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ILogger<DomainEventDispatcher> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider, ILogger<DomainEventDispatcher> logger)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellation)
        where TEvent : IDomainEvent<TEvent>
    {
        var handlers = _serviceProvider.GetServices<IDomainEventHandler<TEvent>>();
        foreach (var handler in handlers)
        {
            try
            {
                await handler.Handle(@event, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "{@Event}", @event);
                throw;
            }
        }
    }
}