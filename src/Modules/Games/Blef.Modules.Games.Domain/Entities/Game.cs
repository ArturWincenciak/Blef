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