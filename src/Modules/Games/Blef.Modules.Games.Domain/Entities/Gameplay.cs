namespace Blef.Modules.Games.Domain.Entities;

internal sealed partial class Gameplay
{
    public Guid Id { get; }
    public List<GamePlayer> Players { get; } = new();
    public Dictionary<int, Deal> Deals { get; } = new();

    public Gameplay(Guid id) =>
        Id = id;

    public void JoinPlayer(Guid playerId, string nick) =>
        Players.Add(new(playerId, nick));

    public void StartNewDeal(int dealNumber, List<Deal.DealPlayer> dealPlayers) =>
        Deals.Add(dealNumber,
            new(dealNumber, new(), new(), Guid.Empty, Guid.Empty));

    public void Bid(int dealNumber, Guid playerId, string pokerHand)
    {
        var deal = Deals[dealNumber];
        var bid = new Deal.Bid(deal.Bids.Count + 1, playerId, pokerHand);
        deal.Bids.Add(bid);
    }

    public void Check(int dealNumber, Guid checkingPlayerId, Guid looserPlayerId)
    {
        var deal = Deals[dealNumber];
        Deals[dealNumber] = deal with
        {
            CheckingPlayerId = checkingPlayerId,
            LooserPlayerId = looserPlayerId
        };
    }
}