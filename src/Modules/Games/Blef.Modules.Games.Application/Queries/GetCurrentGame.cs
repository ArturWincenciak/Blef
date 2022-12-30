using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetCurrentGame(Guid TournamentId) : IQuery<GetCurrentGame.Result>
{
    [UsedImplicitly]
    public sealed record Result(Guid? GameId) : IQueryResult;
}