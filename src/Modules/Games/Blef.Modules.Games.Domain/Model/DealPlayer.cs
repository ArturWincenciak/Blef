namespace Blef.Modules.Games.Domain.Model;

internal sealed record DealPlayer(PlayerId Player, Hand Hand)
{
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));
    public Hand Hand { get; } = Hand ?? throw new ArgumentNullException(nameof(Hand));
}