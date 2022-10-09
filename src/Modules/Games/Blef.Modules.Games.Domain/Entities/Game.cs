namespace Blef.Modules.Games.Domain.Entities;

public sealed class Game
{
    private readonly Dictionary<Guid, Player> _players = new();
    private Guid _looser;
    private string? _lastBid;

    public Guid Id { get; private init; }

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
        if (_lastBid != null && NewBidIsNotHigher(_lastBid, pokerHand))
        {
            throw new Exception($"New bid '{pokerHand}' is not higher than last one '{_lastBid}'");
        }

        _lastBid = pokerHand;
        _players[playerId].Bid(pokerHand);
    }

    private bool NewBidIsNotHigher(string lastBid, string newBid)
    {
        return Bids.Compare(lastBid, newBid) <= 0;
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