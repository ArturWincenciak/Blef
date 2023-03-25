using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class Deal
{
    public DealId Id { get; }
    public List<DealPlayer> _players;

    private Deal(DealId id, List<DealPlayer> players)
    {
        Id = id;
        _players = players;
    }

    public static Deal Create(DealId id, IEnumerable<PlayerId> players) =>
        new Deal(id, players.Select(p => new DealPlayer()).ToList());
}