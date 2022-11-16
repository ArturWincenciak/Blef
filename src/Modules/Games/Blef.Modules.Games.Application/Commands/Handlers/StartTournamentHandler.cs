using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class StartTournamentHandler : ICommandHandler<StartTournament>
{
    private readonly ITournamentsRepository _tournaments;
    private readonly IGamesRepository _games;
    private readonly DeckGenerator _deckGenerator;

    public StartTournamentHandler(ITournamentsRepository tournaments, IGamesRepository games,
        DeckGenerator deckGenerator)
    {
        _tournaments = tournaments;
        _games = games;
        _deckGenerator = deckGenerator;
    }

    public Task Handle(StartTournament command, CancellationToken cancellation)
    {
        var tournament = _tournaments.Get(command.TournamentId);
        tournament.Start();

        var game = Game.Create(_deckGenerator.GetFullDeck(), command.TournamentId);
        foreach (var player in tournament.GetPlayers())
        {
            game.Join(player.PlayerId);
        }

        _games.Add(game);
        tournament.AddGame(game);
        return Task.CompletedTask;
    }
}