namespace Blef.Modules.Games.Domain.Model;

internal sealed record CheckingPlayer(PlayerId Player)
{
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));
}