using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record BidPlaced(Guid GameId, int DealNumber, Guid PlayerId, string PokerHand) : IDomainEvent<BidPlaced>;