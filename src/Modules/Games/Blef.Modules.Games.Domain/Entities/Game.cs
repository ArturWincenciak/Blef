namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Game
{
    private readonly Dictionary<Guid, Player> _players = new();
    private Guid _looser;

    public Guid Id { get; init; }

    public static Game Create() =>
        new() { Id = Guid.NewGuid() };

    public void Join(Guid playerId)
    {
        var card = new Card("Ace", "Diamonds");
        _players.Add(playerId, new Player(new[] { card }));
    }

    public Card[] GetCards(Guid playerId)
    {
        return _players[playerId].DealtCards;
    }

    public void Bid(Guid playerId, string pokerHand)
    {
        // TODO: #67 Check if bid is better than current one
        _players[playerId].Bid(pokerHand);
    }

    public void Check(Guid playerId)
    {
        var player = _players[playerId];
        player.CheckLastBid();
        // TODO: #66 Play with 2 players
        _looser = playerId;
    }

    public Guid GetLooser()
    {
        return _looser;
    }
}