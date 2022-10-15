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
            throw new SimpleBlefException("Cannot join tournament that is already started");
        }

        // TODO: check, cause probably we can handle any number. Think about upper bound like 9?
        if (_players.Count == 2)
        {
            throw new SimpleBlefException("For now only 2 players can play together in tournament");
        }

        if (_players.Contains(playerId))
        {
            throw new SimpleBlefException($"Player '{playerId}' already joined the tournament");
        }

        _players.Add(playerId);
    }

    public void Start()
    {
        if (_isTournamentStarted)
        {
            throw new SimpleBlefException("Tournament cannot be started second time");
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
            throw new SimpleBlefException("Cannot add games for NOT started tournament");
        }

        _games.Add(game);
    }

    public Game GetCurrentGame()
    {
        return _games[^1];
    }
}