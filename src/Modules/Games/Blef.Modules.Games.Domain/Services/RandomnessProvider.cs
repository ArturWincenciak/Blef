namespace Blef.Modules.Games.Domain.Entities;

/// <summary>
///     This class is used, cause we don't want to create Random class
///     every time. It is possible that 'new Random' that are close in code
///     and time will give the same random values.
/// </summary>
internal class RandomnessProvider
{
    private readonly Random _random = new();

    public int GetInt(int exclusiveMaxValue) =>
        _random.Next(minValue: 0, exclusiveMaxValue);
}