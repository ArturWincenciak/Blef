namespace Blef.Modules.Games.Domain;

internal sealed class Games
{
    private readonly List<Guid> _games = new();

    public Guid MakeNewGame()
    {
        var newGameId = Guid.NewGuid();
        _games.Add(newGameId);
        return newGameId;
    }
}