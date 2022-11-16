using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class StartNextGameInTournamentHandler : ICommandHandler<StartNextGameInTournament>
{
    private readonly ITournamentsRepository _tournaments;

    public StartNextGameInTournamentHandler(ITournamentsRepository tournaments, IGamesRepository games, DeckGenerator deckGenerator)
    {
        _tournaments = tournaments;
        _games = games;
        _deckGenerator = deckGenerator;
    }

    private readonly IGamesRepository _games;
    private readonly DeckGenerator _deckGenerator;

    public Task Handle(StartNextGameInTournament command, CancellationToken cancellation)
    {
        // check if last game is finished
        var tournament = _tournaments.Get(command.TournamentId);
        var currentGame = tournament.GetCurrentGame();
        if (currentGame.GetLooser() == null)
        {
            throw new GameNotYetFinishedException(currentGame.Id);
        }
        
        // start next game
        // Add more cards to Looser!!!
        var game = Game.Create(_deckGenerator.GetFullDeck());
        foreach (var player in tournament.GetPlayers())
        {
            game.Join(player.PlayerId);
        }

        _games.Add(game);
        tournament.AddGame(game);
        
        return Task.CompletedTask;
    }
}