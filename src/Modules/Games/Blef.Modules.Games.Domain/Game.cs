namespace Blef.Modules.Games.Domain;

internal sealed class Game
{
    private readonly Dictionary<Guid, Player> _players = new();
    private Guid? _looser;

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
        // TODO: check if bid is better than current one
        _players[playerId].Bid(pokerHand);
    }

    public void Check(Guid playerId)
    {
        var player = _players[playerId];
        player.CheckPreviousBid();
        // TODO: #66 Play with 2 players
        _looser = playerId;
    }

    public Guid? GetLooser()
    {
        return _looser;
    }
}