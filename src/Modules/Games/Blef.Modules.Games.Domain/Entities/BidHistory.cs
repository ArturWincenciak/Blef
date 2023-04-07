using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Entities;

internal class BidHistory
{
    private readonly List<BidItem> _flow = new();
    private int _bidNumber;

    public void OnBid(Bid bid) =>
        _flow.Add(new BidItem(++_bidNumber, bid));

    public IEnumerable<BidItem> GetFlow() =>
        _flow;

    internal record BidItem(int Order, Bid Bid);
}