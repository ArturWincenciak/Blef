using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Entities;

public class Tournament
{
    private readonly List<Guid> _players = new();
    private bool _isTournamentStarted;

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
}