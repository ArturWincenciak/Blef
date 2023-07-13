using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class BidIsNotHigherThenLastOneException : BlefException
{
    public BidIsNotHigherThenLastOneException(DealId dealId, Bid newBid, Bid lastBid)
        : base(
            title: "The bid is not higher than last one",
            detail: $"The new bid '{newBid}' is not higher than last one '{lastBid}'")
    {
    }
}