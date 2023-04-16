namespace Blef.Modules.Games.Domain.ValueObjects;

public sealed record PlayerNick(string Nick)
{
    public string Nick { get; } = string.IsNullOrWhiteSpace(Nick)
        ? throw new ArgumentException("Nick cannot be empty")
        : Nick;
}