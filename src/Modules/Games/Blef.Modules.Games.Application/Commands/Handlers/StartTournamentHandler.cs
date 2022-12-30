using Blef.Modules.Games.Domain.Entities;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class StartTournamentHandler : ICommandHandler<StartTournament>
{
    private readonly Tournaments _tournaments;

    public StartTournamentHandler(Tournaments tournaments) =>
        _tournaments = tournaments;

    public Task Handle(StartTournament command, CancellationToken cancellation)
    {
        _tournaments.Start(command.TournamentId);
        return Task.CompletedTask;
    }
}