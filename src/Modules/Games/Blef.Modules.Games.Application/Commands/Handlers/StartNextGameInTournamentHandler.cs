using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class StartNextGameInTournamentHandler : ICommandHandler<StartNextGameInTournament>
{
    private readonly Tournaments _tournaments;

    public StartNextGameInTournamentHandler(Tournaments tournaments)
    {
        _tournaments = tournaments;
    }

    public Task Handle(StartNextGameInTournament command, CancellationToken cancellation)
    {
        _tournaments.StartNextGame(command.TournamentId);
        
        return Task.CompletedTask;
    }
}