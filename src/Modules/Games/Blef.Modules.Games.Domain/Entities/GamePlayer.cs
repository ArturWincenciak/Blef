using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class GamePlayer
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