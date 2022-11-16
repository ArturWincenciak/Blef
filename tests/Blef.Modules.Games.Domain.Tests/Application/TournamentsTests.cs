using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.Tests.Application.Repositories.InMemory;

namespace Blef.Modules.Games.Domain.Tests.Application;

public class TournamentsTests
{
    private readonly Tournaments _sut;
    private readonly ITournamentsRepository _tournamentsRepository = new InMemoryTournamentsRepository();
    private readonly IGamesRepository _gamesRepository = new InMemoryGamesRepository();

    private readonly Guid _playerId = Guid.NewGuid();

    public TournamentsTests()
    {
        _sut = new Tournaments(_tournamentsRepository, _gamesRepository, new DeckGenerator(new RandomnessProvider()));
    }

    private Tournament SetupStartedTournament()
    {
        var tournament = Tournament.Create();
        _tournamentsRepository.Add(tournament);
        tournament.Join(_playerId);
        _sut.Start(tournament.Id);

        return tournament;
    }

    [Fact]
    public void Should_get_one_more_card_after_loosing_game()
    {
        var tournament = SetupStartedTournament();
        FinishCurrentGame(tournament);

        _sut.StartNextGame(tournament.Id);

        Assert.Equal(2, tournament.GetCurrentGame().GetCards(_playerId).Length);
    }

    private void FinishCurrentGame(Tournament tournament)
    {
        var currentGame = tournament.GetCurrentGame();
        currentGame.Bid(_playerId, "two-pairs:jack,ten");
        currentGame.Check(_playerId);

        Assert.NotNull(currentGame.GetLooser());
    }
}