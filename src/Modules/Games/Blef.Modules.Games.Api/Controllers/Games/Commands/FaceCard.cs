using JetBrains.Annotations;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal enum FaceCard
{
    [UsedImplicitly]
    Nine = 1,

    [UsedImplicitly]
    Ten = 2,

    [UsedImplicitly]
    Jack = 4,

    [UsedImplicitly]
    Queen = 8,

    [UsedImplicitly]
    King = 16,

    [UsedImplicitly]
    Ace = 32
}
