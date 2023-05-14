namespace Blef.Modules.Games.Domain.Model;

// todo: change to internal
public sealed record PlayerId(Guid Id)
{
    public Guid Id { get; } = Id == Guid.Empty
        ? throw new ArgumentException("Player ID cannot be empty")
        : Id;
}