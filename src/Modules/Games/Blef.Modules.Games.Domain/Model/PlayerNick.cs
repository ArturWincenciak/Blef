namespace Blef.Modules.Games.Domain.Model;

internal sealed record PlayerNick(string Nick)
{
    public string Nick { get; } = string.IsNullOrWhiteSpace(Nick)
        ? throw new ArgumentException("Nick cannot be empty")
        : Nick;

    public override string ToString() =>
        Nick;
}