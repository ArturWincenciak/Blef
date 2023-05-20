using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Events;

public interface IDomainEventHandler<in TEvent>
    where TEvent : IDomainEvent
{
    [SuppressMessage(category: "ReSharper", checkId: "UnusedParameter.Global")]
    Task Handle(TEvent @event, CancellationToken cancellation);
}