using Blef.Modules.Games.Domain.Exceptions;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class Game
{
    private readonly Players _players = new();
    private Guid? _looser;
    private string? _lastBid;
    private readonly DealtCards _dealtCards = new();
    private bool _isGameStarted;
    private readonly IDeck _deck;

    public Guid Id { get; }

    private Game(Guid id, IDeck deck)
    {
        Id = id;
        _deck = deck;
    }

    public static Game Create(IDeck deck) =>
        new(Guid.NewGuid(), deck);

    public void Join(Guid playerId)
    {
        if (_isGameStarted)
        {
            throw new JoinGameThatIsAlreadyStartedException(Id, playerId);
        }
        
        if (_players.Count >= 2)
        {
            throw new MaximumNumberOfPlayersHasBeenReachedException(Id);
        }

        if (_players.ContainsId(playerId))
        {
            throw new PlayerAlreadyJoinedTheGameException(Id, playerId);
        }

        var card = _deck.DealCard();
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
            throw new BidIsNotHigherThenLastOneException(Id, pokerHand, _lastBid);
        }

        // TODO: decouple validation logic and parsing the poker hand (parsing contract-based)
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
            // TODO: the same validation should be added for 'Bid' command
            throw new GameIsAlreadyOverException(Id);
        }

        if (_lastBid == null)
        {
            throw new NoBidToCheckException(Id);
        }

        if (_dealtCards.IsBidFulfilled(_lastBid))
        {
            _looser = playerId;
        }
        else
        {
            _looser = _players.GetPreviousPlayer().Id;
        }
    }

    public Guid? GetLooser()
    {
        return _looser;
    }
}