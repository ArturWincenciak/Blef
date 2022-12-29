using Blef.Modules.Games.Domain.Entities.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

public class DealtCards
{
    private readonly List<Card> _cards = new();

    public void Add(IEnumerable<Card> cards) => 
        _cards.AddRange(cards);

    public bool IsBidFulfilled(string lastBid)
    {
        var pokerHand = PokerHandParser.Parse(lastBid);
        return pokerHand.IsOnTable(_cards);
    }
}