using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetGame(GameId Game) : IQuery<GetGame.Result>
{
    [UsedImplicitly]
    public sealed record Result(
        GameStatus Status,
        IEnumerable<Player> Players,
        IEnumerable<Deal> Deals,
        Winner Winner) : IQueryResult;

    [UsedImplicitly]
    public sealed record Player(Guid PlayerId, string Nick, int JoiningOrder);

    public sealed record Deal(int Number, string State, DealResolution DealResolution);

    public sealed record DealResolution(Guid CheckingPlayerId, Guid LooserPlayerId);

    public sealed record Winner(Guid PlayerId);

    public sealed record GameStatus(string State);
}