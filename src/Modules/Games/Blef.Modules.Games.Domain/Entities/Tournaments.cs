using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Repositories;

namespace Blef.Modules.Games.Domain.Entities;

internal class Tournaments
{
    private readonly ITournamentsRepository _tournaments;
    private readonly IGamesRepository _games;
    private readonly DeckGenerator _deckGenerator;

    public Tournaments(ITournamentsRepository tournaments, IGamesRepository games, DeckGenerator deckGenerator)
    {
        _tournaments = tournaments;
        _games = games;
        _deckGenerator = deckGenerator;
    }

    public void Start(Guid tournamentId)
    {
        var tournament = _tournaments.Get(tournamentId);
        tournament.Start();
        StartNextGame(tournament);
    }

    public void StartNextGame(Guid tournamentId)
    {
        var tournament = _tournaments.Get(tournamentId);
        var currentGame = tournament.GetCurrentGame();
        EnsureCurrentGameIsFinished(currentGame);
        MarkLostPlayer(tournament, currentGame);
        StartNextGame(tournament);
    }

    private static void MarkLostPlayer(Tournament tournament, Game currentGame)
    {
        var tournamentPlayer = tournament.GetPlayers().Single(x => x.PlayerId == currentGame.GetLooser());
        tournamentPlayer.MarkLostGame();
    }

    private void StartNextGame(Tournament tournament)
    {
        var game = Game.Create(_deckGenerator.GetFullDeck(), tournament.Id);
        foreach (var player in tournament.GetPlayers())
        {
            var cardsToDeal = player.LostGames + 1;
            game.Promote(player, cardsToDeal);
        }

        _games.Add(game);
        tournament.AddGame(game);
    }

    private static void EnsureCurrentGameIsFinished(Game currentGame)
    {
        if (currentGame.GetLooser() == null)
            throw new GameNotYetFinishedException(currentGame.Id);
    }
}