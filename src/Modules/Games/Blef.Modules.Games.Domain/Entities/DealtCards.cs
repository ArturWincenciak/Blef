namespace Blef.Modules.Games.Domain.Entities;

public class DealtCards
{
    private readonly List<Card> _cards = new();

    public void Add(Card[] cards)
    {
        _cards.AddRange(cards);
    }

    public bool IsBidFulfilled(string lastBid)
    {
        var pokerHand = PokerHand.Parse(lastBid);
        return _cards.Any(x => x.FaceCard == pokerHand.FaceCard);
    }
}