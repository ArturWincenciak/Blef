using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

internal class BidHistory
{
    private readonly List<BidItem> _flow = new();
    private int _bidNumber;

    public void OnBid(PlayerId playerId, PokerHand pokerHand) =>
        _flow.Add(new BidItem(++_bidNumber, playerId, pokerHand));

    public IEnumerable<BidItem> GetFlow() =>
        _flow;

    internal record BidItem(int Order, PlayerId PlayerId, PokerHand PokerHand);
}