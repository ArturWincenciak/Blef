using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.ValueObjects.Dto;

internal record DealFlowResult(
    IEnumerable<DealPlayer> Players,
    IEnumerable<BidHistory.BidItem> Bids,
    PlayerId CheckingPlayerId,
    PlayerId LooserPlayerId);