namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed record GameId
{
    public Guid Id { get; }

    public GameId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Game ID cannot be empty");

        Id = id;
    }
}