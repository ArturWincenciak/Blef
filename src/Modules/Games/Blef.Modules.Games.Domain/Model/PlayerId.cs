namespace Blef.Modules.Games.Domain.Model;

// todo: make internal (here and others similar)
public sealed record PlayerId(Guid Id)
{
    public Guid Id { get; } = Id == Guid.Empty
        ? throw new ArgumentException("Player ID cannot be empty")
        : Id;
}