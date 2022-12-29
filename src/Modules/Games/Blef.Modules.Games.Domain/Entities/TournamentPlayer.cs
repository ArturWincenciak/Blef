namespace Blef.Modules.Games.Domain.Entities;

public class TournamentPlayer
{
    public Guid PlayerId { get; }
    public string Nick { get; }
    public int LostGames { get; private set; }

    public static TournamentPlayer Create(string nick) =>
        new(Guid.NewGuid(), nick);

    private TournamentPlayer(Guid playerId, string nick)
    {
        PlayerId = playerId;
        Nick = nick;
    }

    public void MarkLostGame() =>
        LostGames++;
}