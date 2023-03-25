namespace Blef.Modules.Games.Domain.ValueObjects;

public sealed class GameId
{
    public GameId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Game ID cannot be empty");

        Id = id;
    }

    public Guid Id { get; }
}