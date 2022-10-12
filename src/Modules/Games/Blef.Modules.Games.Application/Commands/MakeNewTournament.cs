using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record MakeNewTournament : ICommand<MakeNewTournament.Result>
{
    public sealed record Result(Guid TournamentId) : ICommandResult;
}