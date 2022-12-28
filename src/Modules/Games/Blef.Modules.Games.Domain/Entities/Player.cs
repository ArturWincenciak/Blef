namespace Blef.Modules.Games.Domain.Entities;

public class Player
{
    public Guid Id { get; }
    public string Nick { get; }
    public Card[] DealtCards { get; }

    private Player(Guid id, string nick, Card[] dealtCards)
    {
        Id = id;
        Nick = nick;
        DealtCards = dealtCards;
    }

    public static Player Create(string nick, Card[] dealtCards) =>
        new (Guid.NewGuid(), nick, dealtCards);

    public static Player Create(TournamentPlayer player, Card[] dealtCards) =>
        new (player.PlayerId, player.Nick, dealtCards);
}