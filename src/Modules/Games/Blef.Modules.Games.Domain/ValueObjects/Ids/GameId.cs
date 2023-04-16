namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed record GameId(Guid Id)
{
    public Guid Id { get; } = Id == Guid.Empty
        ? throw new ArgumentException("Game ID cannot be empty")
        : Id;
}