namespace Blef.Modules.Games.Domain;

public class Player
{
    private readonly List<string> _bids;
    public Card[] DealtCards { get; }

    public Player(Card[] dealtCards)
    {
        DealtCards = dealtCards;
    }

    public void Bid(string pokerHand)
    {
        _bids.Add(pokerHand);
    }
}