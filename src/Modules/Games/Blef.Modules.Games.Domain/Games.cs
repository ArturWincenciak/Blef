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

    public Task Join(Guid gameId, Guid playerId)
    {
        throw new NotImplementedException();
    }
}