using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class GamePlayer
{
    public PlayerId Id { get; }
    public string Nick { get; }

    private GamePlayer(PlayerId id, string nick)
    {
        Id = id;
        Nick = nick;
    }

    public static GamePlayer Create(string nick) =>
        new (new(Guid.NewGuid()), nick);
}