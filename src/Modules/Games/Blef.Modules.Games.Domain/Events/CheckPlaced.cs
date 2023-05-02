using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record CheckPlaced(Guid GameId, int DealNumber, Guid CheckingPlayerId, Guid LooserPlayerId) : IDomainEvent<CheckPlaced>;