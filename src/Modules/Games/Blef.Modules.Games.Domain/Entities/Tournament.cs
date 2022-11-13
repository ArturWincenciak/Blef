using Blef.Modules.Games.Domain.Exceptions;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Entities;

public class Tournament
{
    private readonly List<Guid> _players = new();
    private bool _isTournamentStarted;
    private readonly List<Game> _games = new();

    private Tournament(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public static Tournament Create() =>
        new(Guid.NewGuid());

    public void Join(Guid playerId)
    {
        if (_isTournamentStarted)
        {
            throw new JoinTournamentThatIsAlreadyStartedException(Id, playerId);
        }

        if (_players.Count >= 2)
        {
            throw new MaximumNumberOfTournamentPlayersHasBeenReachedException(Id);
        }

        if (_players.Contains(playerId))
        {
            throw new PlayerAlreadyJoinedTheTournamentException(Id, playerId);
        }

        _players.Add(playerId);
    }

    public void Start()
    {
        if (_isTournamentStarted)
        {
            throw new TournamentHasBeenAlreadyStartedException(Id);
        }

        _isTournamentStarted = true;
    }

    public IEnumerable<Guid> GetPlayers()
    {
        return _players;
    }

    public void AddGame(Game game)
    {
        if (_isTournamentStarted == false)
        {
            // TODO: refactor needed: that exception should be neve reached, previous validation should prevent that
            // that is not business use case domain exception (that is an internal operation exception)
            throw new SimpleBlefException("Cannot add games for NOT started tournament");
        }

        _games.Add(game);
    }

    public Game GetCurrentGame()
    {
        return _games[^1];
    }
}