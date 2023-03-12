using Blef.Modules.Games.Domain.Entities.PokerHands;
using Blef.Modules.Games.Domain.Exceptions;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class Game
{
    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;
    private readonly BidFlowHistory _bidFlowHistory = new();
    private readonly DealtCards _dealtCards = new();
    private readonly IDeck _deck;
    private readonly Players _players = new();
    private bool _isGameStarted;
    private string? _lastBid;
    private Guid? _looser;

    public Guid Id { get; }

    private Game(Guid id, IDeck deck)
    {
        Id = id;
        _deck = deck;
    }

    public static Game Create(IDeck deck) =>
        new(id: Guid.NewGuid(), deck);

    public Player Join(string nick) =>
        Join(nick, cardsToDealCount: 1);

    public Card[] GetCards(Guid playerId) =>
        _players.GetPlayer(playerId).DealtCards;

    private Player Join(string nick, int cardsToDealCount)
    {
        if (_isGameStarted)
            throw new JoinGameThatIsAlreadyStartedException(Id, nick);

        if (_players.Count >= MAX_NUMBER_OF_PLAYERS)
            throw new MaxGamePlayersReachedException(Id);

        if (_players.ContainsNick(nick))
            throw new PlayerAlreadyJoinedTheGameException(Id, nick);

        var cards = _deck.DealCards(cardsToDealCount);
        var player = Player.Create(nick, cards);
        _players.Add(player);
        _dealtCards.Add(cards);

        return player;
    }

    public void Bid(Guid playerId, string pokerHand)
    {
        if (_players.Count < MIN_NUMBER_OF_PLAYERS)
            throw new MinimumGamePlayersNotReachedException(Id);

        if (_looser != null)
            throw new GameIsAlreadyOverException(Id);

        if (_lastBid != null && NewBidIsNotHigher(_lastBid, pokerHand))
            throw new BidIsNotHigherThenLastOneException(Id, pokerHand, _lastBid);

        _players.Bid(playerId);
        _isGameStarted = true;
        _lastBid = pokerHand;

        _bidFlowHistory.OnBid(playerId, pokerHand);
    }

    public void Check(Guid playerId)
    {
        if (_looser != null)
            throw new GameIsAlreadyOverException(Id);

        if (_lastBid == null)
            throw new NoBidToCheckException(Id);

        _looser = _dealtCards.IsBidFulfilled(_lastBid)
            ? playerId
            : _players.GetPreviousPlayer().Id;

        _bidFlowHistory.OnCheck(playerId);
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
            .GetPlayers()
            .Select(player => (
                player.Id,
                player.Nick,
                player.DealtCards))
            .ToArray();

        var bidFlow = _bidFlowHistory.GetFlow();

        return (players, bidFlow.Bids, bidFlow.CheckingPlayerId, _looser ?? Guid.Empty);
    }

    private static bool NewBidIsNotHigher(string lastBid, string newBid)
    {
        var lastPokerHand = PokerHandParser.Parse(lastBid);
        var newPokerHand = PokerHandParser.Parse(newBid);

        return newPokerHand.IsBetterThan(lastPokerHand) == false;
    }

    public Deal NewDeal()
    {
        return new Deal();
    }
}