namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed class PlayerId
{
    public PlayerId(Guid id)
    {
        if (id == Guid.Empty)
            throw new AggregateException("Game player ID cannot be empty");

        Id = id;
    }

    public Guid Id { get; }

    private bool Equals(PlayerId other) =>
        Id.Equals(other.Id);

    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || obj is PlayerId other && Equals(other);

    public override int GetHashCode() =>
        Id.GetHashCode();

    public override string ToString() =>
        Id.ToString();
}