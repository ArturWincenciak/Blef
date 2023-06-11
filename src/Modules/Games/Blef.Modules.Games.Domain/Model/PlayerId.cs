namespace Blef.Modules.Games.Domain.Model;

internal sealed record PlayerId(Guid Id)
{
    public Guid Id { get; } = Id == Guid.Empty
        ? throw new ArgumentException("Player ID cannot be empty")
        : Id;
}
