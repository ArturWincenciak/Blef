namespace Blef.Modules.Games.Domain.Entities;

public class TournamentPlayer
{
    public TournamentPlayer(Guid playerId)
    {
        PlayerId = playerId;
    }

    public void LooseGame()
    {
        LostGames++;
    }

    public Guid PlayerId { get; }
    public int LostGames { get; private set; } = 0;
}