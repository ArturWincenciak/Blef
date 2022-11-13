using Blef.Modules.Games.Domain.Exceptions;

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
            throw new MaxTournamentPlayersReachedException(Id);
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
            throw new InvalidOperationException("Cannot add games for not started tournament");
        }

        _games.Add(game);
    }

    public Game GetCurrentGame()
    {
        return _games[^1];
    }
}