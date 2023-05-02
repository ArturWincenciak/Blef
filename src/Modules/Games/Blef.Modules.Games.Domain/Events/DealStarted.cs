using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record DealStarted(Guid GameId, int DealNumber) : IDomainEvent<DealStarted>;