namespace Blef.Shared.Abstractions.Events;

public interface IDomainEventHandler<in TEvent>
    where TEvent : IDomainEvent
{
    Task Handle(TEvent @event, CancellationToken cancellation);
}