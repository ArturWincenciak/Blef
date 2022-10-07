namespace Blef.Modules.Games.Application;

public class Games
{
    private List<Guid> _games = new();

    public Guid CreateGame()
    {
        var newGameId = Guid.NewGuid();
        _games.Add(newGameId);
        
        return newGameId;
    }
}