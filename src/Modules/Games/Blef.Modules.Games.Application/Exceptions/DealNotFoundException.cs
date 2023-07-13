using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Application.Exceptions;

internal sealed class DealNotFoundException : NotFoundException
{
    public DealNotFoundException(Guid gameId, int dealNumber)
        : base(detail: "Deal not found", instance: $"gameplays/{gameId}/deals/{dealNumber}")
    {
    }
}