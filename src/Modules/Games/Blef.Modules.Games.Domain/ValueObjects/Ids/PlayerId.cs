namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed record PlayerId
{
    public Guid Id { get; }

    public PlayerId(Guid id)
    {
        if (id == Guid.Empty)
            throw new AggregateException("Game player ID cannot be empty");

        Id = id;
    }
}