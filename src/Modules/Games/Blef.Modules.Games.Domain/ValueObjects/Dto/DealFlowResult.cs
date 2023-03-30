using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.ValueObjects.Dto;

internal record DealFlowResult(
    IEnumerable<DealPlayer> Players,
    IEnumerable<BidHistory.BidItem> Bids,
    CheckingPlayer CheckingPlayerId,
    LooserPlayer LooserPlayerId);