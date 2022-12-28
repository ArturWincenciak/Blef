namespace Blef.Modules.Games.Domain.Entities;

public class BidFlowHistory
{
    private int _bidNumber;
    private readonly List<(int Order, Guid PlayerId, string Bid)> _flow = new();
    private Guid _checkingPlayerId;

    public void OnBid(Guid playerId, string bid) => 
        _flow.Add((++_bidNumber, playerId, bid));

    public void OnCheck(Guid playerId) =>
        _checkingPlayerId = playerId;

    public (IReadOnlyCollection<(int Order, Guid PlayerId, string Bid)> Bids, Guid CheckingPlayerId) GetFlow() => 
        (_flow.ToArray(), _checkingPlayerId);
}