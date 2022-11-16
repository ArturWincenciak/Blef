using Blef.Modules.Games.Domain.Entities;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class StartTournamentHandler : ICommandHandler<StartTournament>
{
    public StartTournamentHandler(Tournaments tournaments)
    {
        _tournaments = tournaments;
    }

    private readonly Tournaments _tournaments;

    public Task Handle(StartTournament command, CancellationToken cancellation)
    {
        _tournaments.Start(command.TournamentId);
        return Task.CompletedTask;
    }
}