namespace Blef.Modules.Games.Domain.Entities;

public class Player
{
    private readonly List<string> _bids = new();
    public Guid Id { get; }
    public Card[] DealtCards { get; }

    public Player(Guid id, Card[] dealtCards)
    {
        Id = id;
        DealtCards = dealtCards;
    }

    public void Bid(string pokerHand)
    {
        _bids.Add(pokerHand);
    }

    public string GetLastBid()
    {
        return _bids[^1];
    }

    public void CheckLastBid()
    {
        throw new NotImplementedException();
    }
}