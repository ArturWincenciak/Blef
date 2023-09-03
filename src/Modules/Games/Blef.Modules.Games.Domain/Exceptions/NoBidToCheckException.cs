using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class NoBidToCheckException : BlefException
{
    public NoBidToCheckException(DealId dealId)
        : base(
            title: "There is no bid to check it",
            detail: $"Deal number '{dealId.Deal.Number}' must be started by at least one bid")
    {
    }
}