using Blef.Modules.Games.Domain.Entities.PokerHands;
using Blef.Modules.Games.Domain.Exceptions;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class Deal
{
    private readonly IEnumerable<DealPlayer> _players = new List<DealPlayer>();
    private readonly DealHistory _dealHistory = new();

    private string? _lastBid;
    private Guid? _looser;

    private readonly IDeckService _deckService;

    public Guid Id { get; }
    public int SequenceNumber { get; }

    private Deal(Guid id, int sequenceNumber, IEnumerable<DealPlayer> players)
    {
        Id = id;
        SequenceNumber = sequenceNumber;
        _players = players;
    }

    public static Deal Create(int sequenceNumber, IEnumerable<DealPlayer> players) =>
        new(id: Guid.NewGuid(), sequenceNumber, players);

    public void Bid(Guid playerId, string pokerHand)
    {
        if (_looser != null)
            throw new GameIsAlreadyOverException(Id);

        if (_lastBid != null && NewBidIsNotHigher(_lastBid, pokerHand))
            throw new BidIsNotHigherThenLastOneException(Id, pokerHand, _lastBid);

        _lastBid = pokerHand;

        _dealHistory.OnBid(playerId, pokerHand);
    }

    public void Check(Guid playerId)
    {
        if (_looser != null)
            throw new GameIsAlreadyOverException(Id);

        if (_lastBid == null)
            throw new NoBidToCheckException(Id);

        _looser = null; //todo: decide who is the loser

        _dealHistory.OnCheck(playerId);
    }

    public Guid? GetLooser() =>
        _looser;

    public (
        IReadOnlyCollection<(Guid PlayerId, string Nick, Card[] Cards)> Players,
        IReadOnlyCollection<(int Order, Guid PlayerId, string Bid)> Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId
        ) GetFlow()
    {
        var players = _players
            .Select(player => (
                player.Id,
                player.Nick,
                player.Cards))
            .ToArray();

        var bidFlow = _dealHistory.GetFlow();

        return (players, bidFlow.Bids, bidFlow.CheckingPlayerId, _looser ?? Guid.Empty);
    }

    private static bool NewBidIsNotHigher(string lastBid, string newBid)
    {
        var lastPokerHand = PokerHandParser.Parse(lastBid);
        var newPokerHand = PokerHandParser.Parse(newBid);

        return newPokerHand.IsBetterThan(lastPokerHand) == false;
    }
}