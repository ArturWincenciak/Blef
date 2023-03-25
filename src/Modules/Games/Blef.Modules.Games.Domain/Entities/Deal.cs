using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class Deal
{
    public DealId Id { get; }
    private readonly IEnumerable<DealPlayer> _players = new List<DealPlayer>();

    private Deal(DealId id, IEnumerable<DealPlayer> players)
    {
        Id = id;
        _players = players;
    }

    public static Deal Create(DealId id, IEnumerable<PlayerId> players) =>
        new(id, players.Select(p => new DealPlayer( new(p.Id), DealCards())));

    public IEnumerable<Card> GetCards(PlayerId playerId)
    {
        var player = _players.Single(p => p.Id.Equals(playerId));
        return player.GetCards();
    }

    public void Bid(PlayerId playerId, string pokerHand)
    {
        // todo: ...
    }

    private static IEnumerable<Card> DealCards()
    {
        // todo: add number of cards parameter based on previous game
        // todo: randomise cards
        return new[] {new Card(FaceCard.Ace, Suit.Clubs)};
    }
}