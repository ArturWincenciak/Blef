namespace Blef.Modules.Games.Domain.Model;

// todo: change to internal
public sealed record DealNumber(int Number)
{
    public int Number { get; } = Number < 1
        ? throw new ArgumentException("Deal number cannot be less than one")
        : Number;
}