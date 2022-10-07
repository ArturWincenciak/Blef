namespace Blef.Modules.Games.Domain;

internal sealed class Games
{
    private readonly Dictionary<Guid, Game> _games = new();

    public Guid MakeNewGame()
    {
        var newGameId = Guid.NewGuid();
        _games.Add(newGameId, new());
        return newGameId;
    }

    public Game GetExistingGame(Guid gameId) =>
        _games[gameId];
}