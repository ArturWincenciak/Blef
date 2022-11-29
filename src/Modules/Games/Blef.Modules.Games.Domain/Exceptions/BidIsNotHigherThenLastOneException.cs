using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class BidIsNotHigherThenLastOneException : BlefException
{
    public BidIsNotHigherThenLastOneException(Guid gameId, string newBid, string lastBid)
        : base(
            title: "The bid is not higher than last one",
            detail: $"The new bid '{newBid}' is not higher than last one '{lastBid}'",
            instance: $"/games/{gameId}")
    {
    }
}