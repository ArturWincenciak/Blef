namespace Blef.Modules.Games.Domain.Model;

internal sealed record GameId(Guid Id)
{
    public Guid Id { get; } = Id == Guid.Empty
        ? throw new ArgumentException("Game ID cannot be empty")
        : Id;

    public override string ToString() =>
        Id.ToString();
}
