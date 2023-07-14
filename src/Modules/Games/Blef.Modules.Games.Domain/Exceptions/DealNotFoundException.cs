using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class DealNotFoundException : NotFoundException
{
    public DealNotFoundException(Guid gameId, int dealNumber)
        : base(detail: $"Deal '{dealNumber}' of game '{gameId}' not found")
    {
    }
}