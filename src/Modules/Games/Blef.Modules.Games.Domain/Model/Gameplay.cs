using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Model;

internal sealed class Gameplay
{
    public GameId Id { get; }

    private GamePlayer? _winner = null;
    private readonly List<PlayerEntry> _gamePlayers = new();
    private readonly Dictionary<DealNumber, DealDetails> _deals = new();

    public Gameplay(GameId id) =>
        Id = id;

    public void OnPlayerJoined(GamePlayer gamePlayer) =>
        _gamePlayers.Add(new(gamePlayer, _gamePlayers.Count + 1));

    public void OnDealStarted(DealNumber dealNumber, IEnumerable<DealPlayer> dealPlayers) =>
        _deals.Add(dealNumber, new(dealPlayers, new()));

    public void OnBidPlaced(DealNumber dealNumber, PlayerId playerId, PokerHand pokerHand)
    {
        var deal = _deals[dealNumber];
        var bid = new Bid(pokerHand, playerId);
        var order = deal.Bids.Count + 1;
        deal.Bids.Add(new (order, bid));
    }

    public void OnCheckPlaced(DealNumber dealNumber, CheckingPlayer checkingPlayer, LooserPlayer looserPlayer)
    {
        var deal = _deals[dealNumber];
        _deals[dealNumber] = deal with {
            DealResolution = new (checkingPlayer, looserPlayer)
        };
    }

    public Game GetGameProjection() =>
        new(Status, GamePlayerEntries, Deals, _winner);

    public DealDetails GetDealProjection(DealNumber dealNumber) =>
        _deals[dealNumber];

    public IEnumerable<Card> GetHand(DealNumber dealNumber, PlayerId playerId)
    {
        var deal = _deals[dealNumber];
        var player = deal.Players.Single(player => player.Player == playerId);
        return player.Hand.Cards;
    }

    public void OnGameFinished(GamePlayer winner) =>
        _winner = winner;

    private IEnumerable<PlayerEntry> GamePlayerEntries =>
        _gamePlayers;

    private IEnumerable<DealSummary> Deals =>
        _deals.Select(deal => new DealSummary(
            Number: deal.Key,
            Status: deal.Value.DealResolution is not null
                ? DealStatus.Finished
                : DealStatus.InProgress,
            DealResolution: deal.Value.DealResolution));

    private GameStatus Status
    {
        get
        {
            if (_deals.Count == 0)
                return GameStatus.JoiningPlayers;

            if (_winner is not null)
                return GameStatus.GameIsOver;

            return GameStatus.InProgress;
        }
    }

    internal sealed record Game(
        GameStatus Status,
        IEnumerable<PlayerEntry> GamePlayers,
        IEnumerable<DealSummary> Deals,
        GamePlayer? Winner);

    internal sealed record DealSummary(
        DealNumber Number,
        DealStatus Status,
        DealResolution? DealResolution);

    internal sealed record DealResolution(
        CheckingPlayer CheckingPlayer,
        LooserPlayer Looser);

    internal sealed record PlayerEntry(
        GamePlayer Player,
        int JoiningOrder);

    internal sealed record DealDetails(
        IEnumerable<DealPlayer> Players,
        List<BidRecord> Bids,
        DealResolution? DealResolution = null);

    internal sealed record BidRecord(
        int Order,
        Bid Bid);

    public enum GameStatus
    {
        JoiningPlayers,
        InProgress,
        GameIsOver
    }

    public enum DealStatus
    {
        InProgress,
        Finished
    }
}