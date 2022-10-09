namespace Blef.Modules.Games.Domain.Entities;

public sealed class Game
{
    private readonly Players _players = new();
    private Guid? _looser;
    private string? _lastBid;
    private readonly DealtCards _dealtCards = new();
    private bool _isGameStarted;

    public Guid Id { get; private init; }

    public static Game Create() =>
        new() { Id = Guid.NewGuid() };

    public void Join(Guid playerId)
    {
        if (_isGameStarted)
        {
            throw new Exception("Cannot join to game that is already started");
        }
        
        if (_players.Count == 2)
        {
            throw new Exception("For now only 2 players can play together");
        }

        if (_players.ContainsId(playerId))
        {
            throw new Exception($"Player '{playerId}' already joined the game");
        }

        // TODO: #77 Deal cards from the Deck, temporary simulation of dealing different cards
        var faceCard = _players.Count == 1 ? FaceCard.Ace : FaceCard.King;
        var card = new Card(faceCard, "Diamonds");
        var cards = new[] { card };
        _players.Add(new Player(playerId, cards));
        _dealtCards.Add(cards);
    }

    public Card[] GetCards(Guid playerId)
    {
        return _players.GetPlayer(playerId).DealtCards;
    }

    public void Bid(Guid playerId, string pokerHand)
    {
        if (_lastBid != null && NewBidIsNotHigher(_lastBid, pokerHand))
        {
            throw new Exception($"New bid '{pokerHand}' is not higher than last one '{_lastBid}'");
        }

        // just to check that the bid is Valid.
        PokerHand.Parse(pokerHand);
        
        _players.Bid(playerId, pokerHand);
        _isGameStarted = true;
        _lastBid = pokerHand;
    }

    private bool NewBidIsNotHigher(string lastBid, string newBid)
    {
        return Bids.Compare(lastBid, newBid) <= 0;
    }

    public void Check(Guid playerId)
    {
        if (_looser != null)
        {
            throw new Exception("Cannot check again, game is already over");
        }

        if (_lastBid == null)
        {
            throw new Exception("There is no bid to check it");
        }

        if (_dealtCards.IsBidFulfilled(_lastBid))
        {
            _looser = playerId;
        }
        else
        {
            _looser = _players.GetPreviousPlayer().Id;
        }

        // TODO: it is only for statistics no need to call it in logic
        // player.CheckLastBid();
    }

    public Guid? GetLooser()
    {
        return _looser;
    }
}