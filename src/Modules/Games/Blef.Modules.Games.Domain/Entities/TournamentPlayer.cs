namespace Blef.Modules.Games.Domain.Entities;

public class TournamentPlayer
{
    public Guid PlayerId { get; }
    public string Nick { get; }
    public int LostGames { get; private set; }

    private TournamentPlayer(Guid playerId, string nick)
    {
        PlayerId = playerId;
        Nick = nick;
    }

    public static TournamentPlayer Create(string nick) =>
        new(playerId: Guid.NewGuid(), nick);

    public void MarkLostGame() =>
        LostGames++;
}