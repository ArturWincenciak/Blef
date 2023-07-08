using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetGame(Guid GameId) : IQuery<GetGame.Result>
{
    [UsedImplicitly]
    public sealed record Result(
        GameStatus Status,
        IReadOnlyCollection<Player> Players,
        IReadOnlyCollection<Deal> Deals,
        Winner Winner) : IQueryResult;

    [UsedImplicitly]
    public sealed record Player(Guid PlayerId, string Nick, int JoiningOrder, bool AlreadyLostTheGame);

    [UsedImplicitly]
    public sealed record Deal(int Number, string State, DealResolution DealResolution);

    [UsedImplicitly]
    public sealed record DealResolution(Guid CheckingPlayerId, Guid LooserPlayerId);

    [UsedImplicitly]
    public sealed record Winner(Guid PlayerId);

    [UsedImplicitly]
    public sealed record GameStatus(string State);
}