namespace Blef.Modules.Games.Domain.ValueObjects;

public sealed class PlayerId
{
    public PlayerId(Guid id)
    {
        if (id == Guid.Empty)
            throw new AggregateException("Game player ID cannot be empty");

        Id = id;
    }

    public Guid Id { get; }
}