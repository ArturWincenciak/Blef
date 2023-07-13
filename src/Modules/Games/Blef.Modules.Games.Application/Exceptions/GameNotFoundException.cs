using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Application.Exceptions;

internal sealed class GameNotFoundException : NotFoundException
{
    public GameNotFoundException(Guid gameId)
        : base(detail: $"Game '{gameId}' not found", instance: $"gameplays/{gameId}")
    {
    }
}