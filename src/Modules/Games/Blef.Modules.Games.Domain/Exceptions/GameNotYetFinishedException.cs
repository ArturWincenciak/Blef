using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameNotYetFinishedException : BlefException
{
    public GameNotYetFinishedException(Guid gameId)
        : base(
            title: "Game was not yet finished",
            detail: "Game must be first finished (somebody lost), before starting next game",
            instance: $"/game/{gameId}")
    {
    }
}