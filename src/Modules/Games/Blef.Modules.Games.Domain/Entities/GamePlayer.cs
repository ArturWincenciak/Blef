using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class GamePlayer
{
    public PlayerId Id { get; }
    public PlayerNick Nick { get; }

    private GamePlayer(PlayerId id, PlayerNick nick)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Nick = nick ?? throw new ArgumentNullException(nameof(nick));
    }

    public static GamePlayer Create(PlayerNick nick) =>
        new (new(Guid.NewGuid()), nick);
}