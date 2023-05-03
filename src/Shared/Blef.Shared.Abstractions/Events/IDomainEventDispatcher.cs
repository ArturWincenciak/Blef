namespace Blef.Shared.Abstractions.Events;

public interface IDomainEventDispatcher
{
    Task Dispatch<TEvent>(TEvent @event, CancellationToken cancellation)
        where TEvent : IDomainEvent<TEvent>;

    // Task Dispatch<TEvent>(IEnumerable<TEvent> events, CancellationToken cancellation)
    //     where TEvent : IDomainEvent<TEvent>;
}