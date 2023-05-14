namespace Blef.Modules.Games.Domain.Model;

// todo: change to internal
public sealed record PlayerNick(string Nick)
{
    public string Nick { get; } = string.IsNullOrWhiteSpace(Nick)
        ? throw new ArgumentException("Nick cannot be empty")
        : Nick;

    public override string ToString() =>
        Nick;
}