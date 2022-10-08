namespace Blef.Modules.Games.Domain;

internal sealed class Game
{
    private readonly Dictionary<Guid, Card[]> _players = new();

    public void Join(Guid playerId)
    {
        var card = new Card("Ace", "Diamonds");
        _players.Add(playerId, new[] { card });
    }

    public Card[] GetCards(Guid playerId)
    {
        return _players[playerId];
    }
}