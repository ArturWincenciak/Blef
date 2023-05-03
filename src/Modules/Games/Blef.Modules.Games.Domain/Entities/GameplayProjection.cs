namespace Blef.Modules.Games.Domain.Entities;

internal sealed partial class GameplayProjection
{
    public Guid Id { get; }

    private readonly List<GamePlayer> _players = new();
    private readonly Dictionary<int, Deal> _deals = new();

    public GameplayProjection(Guid id) =>
        Id = id;

    public void OnPlayerJoined(Guid playerId, string nick) =>
        _players.Add(new(playerId, nick));

    public void OnDealStarted(int dealNumber, List<DealPlayer> dealPlayers) =>
        _deals.Add(dealNumber, new(dealNumber, dealPlayers, new()));

    public void OnBidPlaced(int dealNumber, Guid playerId, string pokerHand)
    {
        var deal = _deals[dealNumber];
        var bidNumber = deal.Bids.Count + 1;
        var bid = new Bid(bidNumber, playerId, pokerHand);
        deal.Bids.Add(bid);
    }

    public void OnCheckPlaced(int dealNumber, Guid checkingPlayerId, Guid looserPlayerId)
    {
        var deal = _deals[dealNumber];
        _deals[dealNumber] = deal with {
            CheckingPlayerId = checkingPlayerId,
            LooserPlayerId = looserPlayerId
        };
    }

    public GameProjection GetGameProjection() =>
        new(_players, _deals.Keys);

    public DealProjection GetDealProjection(int dealNumber)
    {
        var deal = _deals[dealNumber];
        return new(deal.Players, deal.Bids, deal.CheckingPlayerId, deal.LooserPlayerId);
    }
}