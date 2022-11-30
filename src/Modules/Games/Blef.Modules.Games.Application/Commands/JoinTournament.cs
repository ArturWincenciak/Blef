using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record JoinTournament(Guid TournamentId, string Nick) : ICommand<JoinTournament.Result>
{
    public sealed record Result(Guid PlayerId, string Nick) : ICommandResult;
}