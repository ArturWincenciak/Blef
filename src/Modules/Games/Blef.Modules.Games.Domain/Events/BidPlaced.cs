using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record BidPlaced(
    GameId Game,
    DealNumber Deal,
    PlayerId Player,
    PokerHand PokerHand) : IDomainEvent<BidPlaced>;