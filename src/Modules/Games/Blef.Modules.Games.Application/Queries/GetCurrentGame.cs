using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetCurrentGame(Guid TournamentId) : IQuery<GetCurrentGame.Result>
{
    public sealed record Result(Guid? GameId) : IQueryResult;
}