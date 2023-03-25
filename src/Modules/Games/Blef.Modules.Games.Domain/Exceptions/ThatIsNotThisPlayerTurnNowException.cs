using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class ThatIsNotThisPlayerTurnNowException : BlefException
{
    public ThatIsNotThisPlayerTurnNowException(PlayerId playerId)
        : base(
            title: "That is not this player's turn now",
            detail: $"Player '{playerId}' should wait for his turn",
            instance: $"/players/{playerId}")
    {
    }
}