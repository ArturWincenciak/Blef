namespace Blef.Shared.Abstractions.Events;

public interface IDomainEvent
{
}

public interface IDomainEvent<TEvent> : IDomainEvent
    where TEvent : IDomainEvent<TEvent>
{
}