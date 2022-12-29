using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.Tests.Application.Repositories.InMemory;

namespace Blef.Modules.Games.Domain.Tests.Application;

public class TournamentsTests
{
    private readonly Tournaments _sut;
    private readonly ITournamentsRepository _tournamentsRepository = new InMemoryTournamentsRepository();
    private readonly IGamesRepository _gamesRepository = new InMemoryGamesRepository();

    private const string PLAYER_NICK = "Player Nick";

    public TournamentsTests() => 
        _sut = new Tournaments(_tournamentsRepository, _gamesRepository, new DeckGenerator(new RandomnessProvider()));

    private (Tournament Tournament, TournamentPlayer Player) SetupStartedTournament()
    {
        var tournament = Tournament.Create();
        _tournamentsRepository.Add(tournament);
        var player = tournament.Join(PLAYER_NICK);
        _sut.Start(tournament.Id);

        return (tournament, player);
    }

    [Fact]
    public void Should_get_one_more_card_after_loosing_game()
    {
        var (tournament, player) = SetupStartedTournament();
        FinishCurrentGame(tournament, player);

        _sut.StartNextGame(tournament.Id);

        Assert.Equal(2, tournament.GetCurrentGame().GetCards(player.PlayerId).Length);
    }

    private static void FinishCurrentGame(Tournament tournament, TournamentPlayer player)
    {
        var currentGame = tournament.GetCurrentGame();
        currentGame.Bid(player.PlayerId, "two-pairs:jack,ten");
        currentGame.Check(player.PlayerId);

        Assert.NotNull(currentGame.GetLooser());
    }
}