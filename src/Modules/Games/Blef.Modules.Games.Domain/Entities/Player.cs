using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Domain.Entities;

public class Player
{
    private readonly List<string> _bids = new();
    public Guid Id { get; }
    public string Nick { get; }
    public Card[] DealtCards { get; }

    private Player(Guid id, string nick, Card[] dealtCards)
    {
        Id = id;
        Nick = nick;
        DealtCards = dealtCards;
    }

    public static Player Create(string nick, Card[] dealtCards) =>
        new (Guid.NewGuid(), nick, dealtCards);

    public static Player Create(TournamentPlayer player, Card[] dealtCards) =>
        new (player.PlayerId, player.Nick, dealtCards);

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