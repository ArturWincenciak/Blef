using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.Tests.Application.Repositories.InMemory;

namespace Blef.Modules.Games.Domain.Tests.Application;

public class TournamentsTests
{
    private readonly IGamesRepository _gamesRepository = new InMemoryGamesRepository();
    private readonly Tournaments _sut;
    private readonly ITournamentsRepository _tournamentsRepository = new InMemoryTournamentsRepository();

    public TournamentsTests() =>
        _sut = new Tournaments(_tournamentsRepository, _gamesRepository,
            deckGenerator: new DeckGenerator(new RandomnessProvider()));

    [Fact]
    public void Should_Get_One_More_Card_After_Loosing_Game()
    {
        // arrange
        var (tournament, players) = SetupStartedTournament();
        FinishCurrentGame(tournament, players[0]);

        // act
        _sut.StartNextGame(tournament.Id);

        // assert
        Assert.Equal(expected: 2, tournament.GetCurrentGame().GetCards(players[0].PlayerId).Length);
    }

    private (Tournament Tournament, TournamentPlayer[] Players) SetupStartedTournament()
    {
        var tournament = Tournament.Create();
        _tournamentsRepository.Add(tournament);

        var firstPlayer = tournament.Join("First Player Nick");
        var secondPlayer = tournament.Join("Second Player Nick");

        _sut.Start(tournament.Id);

        return (tournament, new[] {firstPlayer, secondPlayer});
    }

    private static void FinishCurrentGame(Tournament tournament, TournamentPlayer player)
    {
        var currentGame = tournament.GetCurrentGame();
        currentGame.Bid(player.PlayerId, pokerHand: "two-pairs:jack,ten");
        currentGame.Check(player.PlayerId); //bug: cannot bid and check by the same player

        Assert.NotNull(currentGame.GetLooser());
    }
}