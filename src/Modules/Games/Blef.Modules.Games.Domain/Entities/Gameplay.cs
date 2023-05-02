namespace Blef.Modules.Games.Domain.Entities;

internal sealed partial class Gameplay
{
    public Guid Id { get; }

    private List<GamePlayer> _players = new();
    private readonly Dictionary<int, Deal> _deals = new();

    public Gameplay(Guid id) =>
        Id = id;

    public void JoinPlayer(Guid playerId, string nick) =>
        _players.Add(new(playerId, nick));

    public void StartNewDeal(int dealNumber, List<Deal.DealPlayer> dealPlayers) =>
        _deals.Add(dealNumber, new(dealNumber, dealPlayers, new(), Guid.Empty, Guid.Empty));

    public void Bid(int dealNumber, Guid playerId, string pokerHand)
    {
        var deal = _deals[dealNumber];
        var bid = new Deal.Bid(deal.Bids.Count + 1, playerId, pokerHand);
        deal.Bids.Add(bid);
    }

    public void Check(int dealNumber, Guid checkingPlayerId, Guid looserPlayerId)
    {
        var deal = _deals[dealNumber];
        _deals[dealNumber] = deal with {
            CheckingPlayerId = checkingPlayerId,
            LooserPlayerId = looserPlayerId
        };
    }
}